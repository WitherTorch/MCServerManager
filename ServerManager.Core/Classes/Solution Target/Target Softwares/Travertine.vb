Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class Travertine
    Inherits BungeeCord
    Const TravertineApiJson = "https://papermc.io/api/v1/travertine"
    Const TravertineDownloadLink = "https://papermc.io/api/v1/travertine/{0}/{1}/download"
    Shared latestTravertineVersion As Integer = 0
    Shared latestTravertineBranch As String = ""
    Protected TravertineBranch As String = ""
    Friend Shared Shadows Sub GetVersionList()
        Try
            Dim client As New Net.WebClient()
            latestTravertineBranch = JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson)).GetValue("versions").First.ToString()
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(TravertineApiJson & "/" & latestTravertineBranch))
            latestTravertineVersion = CType(jsonObject.GetValue("builds"), JObject).GetValue("latest")
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Public Overrides Function GetTargetFileName() As String
        Return "Travertine.jar"
    End Function

    Public Overrides Function GetInternalName() As String
        Return "Travertine"
    End Function
    Public Overrides Function GetAvailableVersion() As String
        Return latestTravertineVersion
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return "Travertine #" & SolutionTargetVersion
    End Function

    Protected Overrides Sub OnReadSolutionInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                MemoryMax = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                MemoryMin = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "travertine-branch"
                TravertineBranch = value
        End Select
    End Sub
    Public Overrides Function GetAdditionalSolutionInfo() As String()
        Return New String() {"server-memory-max=" & MemoryMax,
                                                  "server-memory-min=" & MemoryMin,
                                                  "travertine-branch=" & TravertineBranch}
    End Function
    Public Overrides Function DownloadAndInstallTarget(targetSolutionVersion As String) As ServerDownloadTask
        Dim DownloadPath As String = IO.Path.Combine(IIf(SolutionPath.EndsWith(seperator), SolutionPath, SolutionPath & seperator), "Travertine.jar")
        Dim task As New ServerDownloadTask
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnTargetDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnTargetDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Call OnTargetDownloadEnd(False)
                                               TravertineBranch = latestTravertineBranch
                                               SolutionTargetVersion = targetSolutionVersion
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnTargetDownloadStart()
                                         End Sub
        task.Download(String.Format(TravertineApiJson, latestTravertineBranch, latestTravertineVersion), DownloadPath)
        Return task
    End Function
    Public Overrides Function UpdateTarget() As ServerDownloadTask
        Return DownloadAndInstallTarget(latestTravertineVersion)
    End Function
End Class
