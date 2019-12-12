Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class Waterfall
    Inherits BungeeCord
    Const WaterfallApiJson = "https://papermc.io/api/v1/waterfall"
    Const WaterfallDownloadLink = "https://papermc.io/api/v1/waterfall/{0}/{1}/download"
    Shared latestWaterfallVersion As Integer = 0
    Shared latestWaterfallBranch As String = ""
    Protected WaterfallBranch As String = ""
    Friend Shared Shadows Sub GetVersionList()
        Try
            Dim client As New Net.WebClient()
            latestWaterfallBranch = JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson)).GetValue("versions").First.ToString()
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(WaterfallApiJson & "/" & latestWaterfallBranch))
            latestWaterfallVersion = CType(jsonObject.GetValue("builds"), JObject).GetValue("latest")
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Public Overrides Function GetTargetFileName() As String
        Return "Waterfall.jar"
    End Function

    Public Overrides Function GetInternalName() As String
        Return "Waterfall"
    End Function
    Public Overrides Function GetAvailableVersion() As String
        Return latestWaterfallVersion
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return "Waterfall #" & SolutionTargetVersion
    End Function

    Protected Overrides Sub OnReadSolutionInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                MemoryMax = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                MemoryMin = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "waterfall-branch"
                WaterfallBranch = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalSolutionInfo() As String()
        Return New String() {"server-memory-max=" & MemoryMax,
                                                  "server-memory-min=" & MemoryMin,
                                                  "waterfall-branch=" & WaterfallBranch}
    End Function
    Public Overrides Function DownloadAndInstallTarget(targetSolutionVersion As String) As ServerDownloadTask
        Dim DownloadPath As String = IO.Path.Combine(IIf(SolutionPath.EndsWith(seperator), SolutionPath, SolutionPath & seperator), "Waterfall.jar")
        Dim task As New ServerDownloadTask
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnTargetDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnTargetDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Call OnTargetDownloadEnd(False)
                                               WaterfallBranch = latestWaterfallBranch
                                               SolutionTargetVersion = targetSolutionVersion
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnTargetDownloadStart()
                                         End Sub
        task.Download(String.Format(WaterfallDownloadLink, latestWaterfallBranch, latestWaterfallVersion), DownloadPath)
        Return task
    End Function
    Public Overrides Function UpdateTarget() As ServerDownloadTask
        Return DownloadAndInstallTarget(latestWaterfallVersion)
    End Function
End Class
