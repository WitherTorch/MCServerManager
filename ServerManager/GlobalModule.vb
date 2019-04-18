Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Module GlobalModule
    Friend TestForm As ServerCheckingForm
    Public Const SERVER_MANAGER_VER As String = "1.6 Alpha 8"
    Friend Manager As Manager
#Region "Server/Solution List"
    Friend JavaServerDirs As String = ReadAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "servers.txt"))
    Friend BedrockServerDirs As String = ReadAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "peServers.txt"))
    Friend SolutionDirs As String = ReadAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "solutions.txt"))
#End Region
#Region "Version List"
    Friend VanillaVersionDict As New Dictionary(Of String, String)
    Friend ForgeVersionDict As New Dictionary(Of Version, Version)
    Friend SpigotVersionDict As New Dictionary(Of String, String)
    Friend SpigotGitVersionList As New List(Of String)
    Friend CraftBukkitVersionDict As New Dictionary(Of String, String)
    Friend NukkitVersion As String
    Friend NukkitVersionUrl As String
    Friend SpongeVanillaVersionList As New Dictionary(Of String, SpongeVersion)
    Friend VanillaBedrockVersion As Version
    Friend PaperVersionDict As New Dictionary(Of Version, String)
    Friend AkarinVersionList As New List(Of Version)
#End Region
#Region "Modpack List"
    Friend FeedTheBeastPackList As New Dictionary(Of String, String)
    Friend ATPackList As New Dictionary(Of String, String)
#End Region
#Region "General Settings"
    Friend ServerMemoryMin As Decimal = 1024
    Friend ServerMemoryMax As Decimal = 1024
    Friend BungeeCordMemoryMin As Decimal = 16
    Friend BungeeCordMemoryMax As Decimal = 16
    Friend JavaArguments As String = ""
    Friend JavaPath As String = ""
#End Region
#Region "Tools"
    Friend GitBashPath As String = ""
#End Region
    Public GeneratorSolutionCodes As String() = {
        "{""coordinateScale"":684.412,""heightScale"":684.412,""lowerLimitScale"":512.0,""upperLimitScale"":512.0,""depthNoiseScaleX"":200.0,""depthNoiseScaleZ"":200.0,""depthNoiseScaleExponent"":0.5,""mainNoiseScaleX"":5000.0,""mainNoiseScaleY"":1000.0,""mainNoiseScaleZ"":5000.0,""baseSize"":8.5,""stretchY"":8.0,""biomeDepthWeight"":2.0,""biomeDepthOffset"":0.5,""biomeScaleWeight"":2.0,""biomeScaleOffset"":0.375,""seaLevel"":255,""useCaves"":true,""useDungeons"":true,""dungeonChance"":8,""useStrongholds"":true,""useVillages"":true,""useMineShafts"":true,""useTemples"":true,""useRavines"":true,""useWaterLakes"":true,""waterLakeChance"":4,""useLavaLakes"":true,""lavaLakeChance"":80,""useLavaOceans"":false,""fixedBiome"":-1,""biomeSize"":4,""riverSize"":4,""dirtSize"":33,""dirtCount"":10,""dirtMinHeight"":0,""dirtMaxHeight"":256,""gravelSize"":33,""gravelCount"":8,""gravelMinHeight"":0,""gravelMaxHeight"":256,""graniteSize"":33,""graniteCount"":10,""graniteMinHeight"":0,""graniteMaxHeight"":80,""dioriteSize"":33,""dioriteCount"":10,""dioriteMinHeight"":0,""dioriteMaxHeight"":80,""andesiteSize"":33,""andesiteCount"":10,""andesiteMinHeight"":0,""andesiteMaxHeight"":80,""coalSize"":17,""coalCount"":20,""coalMinHeight"":0,""coalMaxHeight"":128,""ironSize"":9,""ironCount"":20,""ironMinHeight"":0,""ironMaxHeight"":64,""goldSize"":9,""goldCount"":2,""goldMinHeight"":0,""goldMaxHeight"":32,""redstoneSize"":8,""redstoneCount"":8,""redstoneMinHeight"":0,""redstoneMaxHeight"":16,""diamondSize"":8,""diamondCount"":1,""diamondMinHeight"":0,""diamondMaxHeight"":16,""lapisSize"":7,""lapisCount"":1,""lapisCenterHeight"":16,""lapisSpread"":16}",
        "{""coordinateScale"":3000.0,""heightScale"":6000.0,""lowerLimitScale"":512.0,""upperLimitScale"":250.0,""depthNoiseScaleX"":200.0,""depthNoiseScaleZ"":200.0,""depthNoiseScaleExponent"":0.5,""mainNoiseScaleX"":80.0,""mainNoiseScaleY"":160.0,""mainNoiseScaleZ"":80.0,""baseSize"":8.5,""stretchY"":10.0,""biomeDepthWeight"":1.0,""biomeDepthOffset"":0.0,""biomeScaleWeight"":1.0,""biomeScaleOffset"":0.0,""seaLevel"":63,""useCaves"":true,""useDungeons"":true,""dungeonChance"":8,""useStrongholds"":true,""useVillages"":true,""useMineShafts"":true,""useTemples"":true,""useRavines"":true,""useWaterLakes"":true,""waterLakeChance"":4,""useLavaLakes"":true,""lavaLakeChance"":80,""useLavaOceans"":false,""fixedBiome"":-1,""biomeSize"":4,""riverSize"":4,""dirtSize"":33,""dirtCount"":10,""dirtMinHeight"":0,""dirtMaxHeight"":256,""gravelSize"":33,""gravelCount"":8,""gravelMinHeight"":0,""gravelMaxHeight"":256,""graniteSize"":33,""graniteCount"":10,""graniteMinHeight"":0,""graniteMaxHeight"":80,""dioriteSize"":33,""dioriteCount"":10,""dioriteMinHeight"":0,""dioriteMaxHeight"":80,""andesiteSize"":33,""andesiteCount"":10,""andesiteMinHeight"":0,""andesiteMaxHeight"":80,""coalSize"":17,""coalCount"":20,""coalMinHeight"":0,""coalMaxHeight"":128,""ironSize"":9,""ironCount"":20,""ironMinHeight"":0,""ironMaxHeight"":64,""goldSize"":9,""goldCount"":2,""goldMinHeight"":0,""goldMaxHeight"":32,""redstoneSize"":8,""redstoneCount"":8,""redstoneMinHeight"":0,""redstoneMaxHeight"":16,""diamondSize"":8,""diamondCount"":1,""diamondMinHeight"":0,""diamondMaxHeight"":16,""lapisSize"":7,""lapisCount"":1,""lapisCenterHeight"":16,""lapisSpread"":16}",
        "{""coordinateScale"":684.412,""heightScale"":684.412,""lowerLimitScale"":512.0,""upperLimitScale"":512.0,""depthNoiseScaleX"":200.0,""depthNoiseScaleZ"":200.0,""depthNoiseScaleExponent"":0.5,""mainNoiseScaleX"":5000.0,""mainNoiseScaleY"":1000.0,""mainNoiseScaleZ"":5000.0,""baseSize"":8.5,""stretchY"":5.0,""biomeDepthWeight"":2.0,""biomeDepthOffset"":1.0,""biomeScaleWeight"":4.0,""biomeScaleOffset"":1.0,""seaLevel"":63,""useCaves"":true,""useDungeons"":true,""dungeonChance"":8,""useStrongholds"":true,""useVillages"":true,""useMineShafts"":true,""useTemples"":true,""useRavines"":true,""useWaterLakes"":true,""waterLakeChance"":4,""useLavaLakes"":true,""lavaLakeChance"":80,""useLavaOceans"":false,""fixedBiome"":-1,""biomeSize"":4,""riverSize"":4,""dirtSize"":33,""dirtCount"":10,""dirtMinHeight"":0,""dirtMaxHeight"":256,""gravelSize"":33,""gravelCount"":8,""gravelMinHeight"":0,""gravelMaxHeight"":256,""graniteSize"":33,""graniteCount"":10,""graniteMinHeight"":0,""graniteMaxHeight"":80,""dioriteSize"":33,""dioriteCount"":10,""dioriteMinHeight"":0,""dioriteMaxHeight"":80,""andesiteSize"":33,""andesiteCount"":10,""andesiteMinHeight"":0,""andesiteMaxHeight"":80,""coalSize"":17,""coalCount"":20,""coalMinHeight"":0,""coalMaxHeight"":128,""ironSize"":9,""ironCount"":20,""ironMinHeight"":0,""ironMaxHeight"":64,""goldSize"":9,""goldCount"":2,""goldMinHeight"":0,""goldMaxHeight"":32,""redstoneSize"":8,""redstoneCount"":8,""redstoneMinHeight"":0,""redstoneMaxHeight"":16,""diamondSize"":8,""diamondCount"":1,""diamondMinHeight"":0,""diamondMaxHeight"":16,""lapisSize"":7,""lapisCount"":1,""lapisCenterHeight"":16,""lapisSpread"":16}",
        "{""coordinateScale"":738.41864,""heightScale"":157.69133,""lowerLimitScale"":1254.1643,""upperLimitScale"":801.4267,""depthNoiseScaleX"":374.93652,""depthNoiseScaleZ"":288.65228,""depthNoiseScaleExponent"":1.2092624,""mainNoiseScaleX"":1355.9908,""mainNoiseScaleY"":745.5343,""mainNoiseScaleZ"":1183.464,""baseSize"":1.8758626,""stretchY"":1.7137525,""biomeDepthWeight"":1.7553768,""biomeDepthOffset"":3.4701107,""biomeScaleWeight"":1.0,""biomeScaleOffset"":2.535211,""seaLevel"":63,""useCaves"":true,""useDungeons"":true,""dungeonChance"":8,""useStrongholds"":true,""useVillages"":true,""useMineShafts"":true,""useTemples"":true,""useRavines"":true,""useWaterLakes"":true,""waterLakeChance"":4,""useLavaLakes"":true,""lavaLakeChance"":80,""useLavaOceans"":false,""fixedBiome"":-1,""biomeSize"":4,""riverSize"":4,""dirtSize"":33,""dirtCount"":10,""dirtMinHeight"":0,""dirtMaxHeight"":256,""gravelSize"":33,""gravelCount"":8,""gravelMinHeight"":0,""gravelMaxHeight"":256,""graniteSize"":33,""graniteCount"":10,""graniteMinHeight"":0,""graniteMaxHeight"":80,""dioriteSize"":33,""dioriteCount"":10,""dioriteMinHeight"":0,""dioriteMaxHeight"":80,""andesiteSize"":33,""andesiteCount"":10,""andesiteMinHeight"":0,""andesiteMaxHeight"":80,""coalSize"":17,""coalCount"":20,""coalMinHeight"":0,""coalMaxHeight"":128,""ironSize"":9,""ironCount"":20,""ironMinHeight"":0,""ironMaxHeight"":64,""goldSize"":9,""goldCount"":2,""goldMinHeight"":0,""goldMaxHeight"":32,""redstoneSize"":8,""redstoneCount"":8,""redstoneMinHeight"":0,""redstoneMaxHeight"":16,""diamondSize"":8,""diamondCount"":1,""diamondMinHeight"":0,""diamondMaxHeight"":16,""lapisSize"":7,""lapisCount"":1,""lapisCenterHeight"":16,""lapisSpread"":16}",
        "{""coordinateScale"":684.412,""heightScale"":684.412,""lowerLimitScale"":512.0,""upperLimitScale"":512.0,""depthNoiseScaleX"":200.0,""depthNoiseScaleZ"":200.0,""depthNoiseScaleExponent"":0.5,""mainNoiseScaleX"":1000.0,""mainNoiseScaleY"":3000.0,""mainNoiseScaleZ"":1000.0,""baseSize"":8.5,""stretchY"":10.0,""biomeDepthWeight"":1.0,""biomeDepthOffset"":0.0,""biomeScaleWeight"":1.0,""biomeScaleOffset"":0.0,""seaLevel"":20,""useCaves"":true,""useDungeons"":true,""dungeonChance"":8,""useStrongholds"":true,""useVillages"":true,""useMineShafts"":true,""useTemples"":true,""useRavines"":true,""useWaterLakes"":true,""waterLakeChance"":4,""useLavaLakes"":true,""lavaLakeChance"":80,""useLavaOceans"":false,""fixedBiome"":-1,""biomeSize"":4,""riverSize"":4,""dirtSize"":33,""dirtCount"":10,""dirtMinHeight"":0,""dirtMaxHeight"":256,""gravelSize"":33,""gravelCount"":8,""gravelMinHeight"":0,""gravelMaxHeight"":256,""graniteSize"":33,""graniteCount"":10,""graniteMinHeight"":0,""graniteMaxHeight"":80,""dioriteSize"":33,""dioriteCount"":10,""dioriteMinHeight"":0,""dioriteMaxHeight"":80,""andesiteSize"":33,""andesiteCount"":10,""andesiteMinHeight"":0,""andesiteMaxHeight"":80,""coalSize"":17,""coalCount"":20,""coalMinHeight"":0,""coalMaxHeight"":128,""ironSize"":9,""ironCount"":20,""ironMinHeight"":0,""ironMaxHeight"":64,""goldSize"":9,""goldCount"":2,""goldMinHeight"":0,""goldMaxHeight"":32,""redstoneSize"":8,""redstoneCount"":8,""redstoneMinHeight"":0,""redstoneMaxHeight"":16,""diamondSize"":8,""diamondCount"":1,""diamondMinHeight"":0,""diamondMaxHeight"":16,""lapisSize"":7,""lapisCount"":1,""lapisCenterHeight"":16,""lapisSpread"":16}",
        "{""coordinateScale"":684.412,""heightScale"":684.412,""lowerLimitScale"":64.0,""upperLimitScale"":2.0,""depthNoiseScaleX"":200.0,""depthNoiseScaleZ"":200.0,""depthNoiseScaleExponent"":0.5,""mainNoiseScaleX"":80.0,""mainNoiseScaleY"":160.0,""mainNoiseScaleZ"":80.0,""baseSize"":8.5,""stretchY"":12.0,""biomeDepthWeight"":1.0,""biomeDepthOffset"":0.0,""biomeScaleWeight"":1.0,""biomeScaleOffset"":0.0,""seaLevel"":6,""useCaves"":true,""useDungeons"":true,""dungeonChance"":8,""useStrongholds"":true,""useVillages"":true,""useMineShafts"":true,""useTemples"":true,""useRavines"":true,""useWaterLakes"":true,""waterLakeChance"":4,""useLavaLakes"":true,""lavaLakeChance"":80,""useLavaOceans"":false,""fixedBiome"":-1,""biomeSize"":4,""riverSize"":4,""dirtSize"":33,""dirtCount"":10,""dirtMinHeight"":0,""dirtMaxHeight"":256,""gravelSize"":33,""gravelCount"":8,""gravelMinHeight"":0,""gravelMaxHeight"":256,""graniteSize"":33,""graniteCount"":10,""graniteMinHeight"":0,""graniteMaxHeight"":80,""dioriteSize"":33,""dioriteCount"":10,""dioriteMinHeight"":0,""dioriteMaxHeight"":80,""andesiteSize"":33,""andesiteCount"":10,""andesiteMinHeight"":0,""andesiteMaxHeight"":80,""coalSize"":17,""coalCount"":20,""coalMinHeight"":0,""coalMaxHeight"":128,""ironSize"":9,""ironCount"":20,""ironMinHeight"":0,""ironMaxHeight"":64,""goldSize"":9,""goldCount"":2,""goldMinHeight"":0,""goldMaxHeight"":32,""redstoneSize"":8,""redstoneCount"":8,""redstoneMinHeight"":0,""redstoneMaxHeight"":16,""diamondSize"":8,""diamondCount"":1,""diamondMinHeight"":0,""diamondMaxHeight"":16,""lapisSize"":7,""lapisCount"":1,""lapisCenterHeight"":16,""lapisSpread"":16}",
        "{""coordinateScale"":684.412,""heightScale"":684.412,""lowerLimitScale"":512.0,""upperLimitScale"":512.0,""depthNoiseScaleX"":200.0,""depthNoiseScaleZ"":200.0,""depthNoiseScaleExponent"":0.5,""mainNoiseScaleX"":80.0,""mainNoiseScaleY"":160.0,""mainNoiseScaleZ"":80.0,""baseSize"":8.5,""stretchY"":12.0,""biomeDepthWeight"":1.0,""biomeDepthOffset"":0.0,""biomeScaleWeight"":1.0,""biomeScaleOffset"":0.0,""seaLevel"":40,""useCaves"":true,""useDungeons"":true,""dungeonChance"":8,""useStrongholds"":true,""useVillages"":true,""useMineShafts"":true,""useTemples"":true,""useRavines"":true,""useWaterLakes"":true,""waterLakeChance"":80,""useLavaLakes"":true,""lavaLakeChance"":4,""useLavaOceans"":false,""fixedBiome"":-1,""biomeSize"":4,""riverSize"":4,""dirtSize"":33,""dirtCount"":10,""dirtMinHeight"":0,""dirtMaxHeight"":256,""gravelSize"":33,""gravelCount"":8,""gravelMinHeight"":0,""gravelMaxHeight"":256,""graniteSize"":33,""graniteCount"":10,""graniteMinHeight"":0,""graniteMaxHeight"":80,""dioriteSize"":33,""dioriteCount"":10,""dioriteMinHeight"":0,""dioriteMaxHeight"":80,""andesiteSize"":33,""andesiteCount"":10,""andesiteMinHeight"":0,""andesiteMaxHeight"":80,""coalSize"":17,""coalCount"":20,""coalMinHeight"":0,""coalMaxHeight"":128,""ironSize"":9,""ironCount"":20,""ironMinHeight"":0,""ironMaxHeight"":64,""goldSize"":9,""goldCount"":2,""goldMinHeight"":0,""goldMaxHeight"":32,""redstoneSize"":8,""redstoneCount"":8,""redstoneMinHeight"":0,""redstoneMaxHeight"":16,""diamondSize"":8,""diamondCount"":1,""diamondMinHeight"":0,""diamondMaxHeight"":16,""lapisSize"":7,""lapisCount"":1,""lapisCenterHeight"":16,""lapisSpread"":16}"}
    Enum SpongeVersionType
        None
        Dev
        Beta
        RC
        Release
    End Enum
    Class SpongeVersion
        Implements IComparable(Of SpongeVersion)
        Public Property OriginalString As String
        Public Property MinecraftVersion As Version
        Public Property ForgeBranch As Integer
        Public Property SpongeVersion As Version
        Public Property SpongeVersionType As SpongeVersionType
        Public Property isSpongeForge As Boolean = False
        Public Property Build As Integer
        Sub New(originalString As String,
                minecraftVersion As String,
                spongeVersion As String,
                versionType As SpongeVersionType,
                Optional spongeBuildVersion As Integer = 0,
                Optional forgeBranch As Integer = 0,
                Optional isSpongeForge As Boolean = False)

            Me.MinecraftVersion = New Version(minecraftVersion)
            Me.SpongeVersion = New Version(spongeVersion)
            SpongeVersionType = versionType
            Build = spongeBuildVersion
            _OriginalString = originalString
            Me.ForgeBranch = forgeBranch
            Me.isSpongeForge = isSpongeForge
        End Sub
        Shared Operator =(left As SpongeVersion, right As SpongeVersion)
            Return (left.MinecraftVersion = right.MinecraftVersion) _
            And (left.SpongeVersion = right.SpongeVersion) _
            And (left.SpongeVersionType = right.SpongeVersionType) _
            And (left.Build = right.Build)
        End Operator
        Shared Operator <>(left As SpongeVersion, right As SpongeVersion)
            Return Not ((left.MinecraftVersion = right.MinecraftVersion) _
            And (left.SpongeVersion = right.SpongeVersion) _
            And (left.SpongeVersionType = right.SpongeVersionType) _
            And (left.Build = right.Build))
        End Operator
        Shared Operator <(left As SpongeVersion, right As SpongeVersion)
            If left.MinecraftVersion > right.MinecraftVersion Then
                Return False
            ElseIf left.MinecraftVersion > right.MinecraftVersion Then
                If left.SpongeVersion > right.SpongeVersion Then
                    Return False
                ElseIf left.SpongeVersion = right.SpongeVersion Then
                    If left.SpongeVersionType > right.SpongeVersionType Then
                        Return False
                    ElseIf left.SpongeVersionType = right.SpongeVersionType Then
                        If left.Build >= right.Build Then
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Operator
        Shared Operator >(left As SpongeVersion, right As SpongeVersion)
            If left.MinecraftVersion > right.MinecraftVersion Then
                Return True
            ElseIf left.MinecraftVersion > right.MinecraftVersion Then
                If left.SpongeVersion > right.SpongeVersion Then
                    Return True
                ElseIf left.SpongeVersion = right.SpongeVersion Then
                    If left.SpongeVersionType > right.SpongeVersionType Then
                        Return True
                    ElseIf left.SpongeVersionType = right.SpongeVersionType Then
                        If left.Build > right.Build Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If

                Else
                    Return False
                End If
            Else
                Return False
            End If
        End Operator
        Function GetDownloadUrl() As String
            If isSpongeForge Then
                Return String.Format("https://repo.spongepowered.org/maven/org/spongepowered/spongeforge/{0}/spongeforge-{0}.jar", _OriginalString)
            Else
                Return String.Format("https://repo.spongepowered.org/maven/org/spongepowered/spongevanilla/{0}/spongevanilla-{0}.jar", _OriginalString)
            End If
        End Function

        Public Function CompareTo(other As SpongeVersion) As Integer Implements IComparable(Of SpongeVersion).CompareTo
            If Me > other Then
                Return -1
            ElseIf Me = other Then
                Return 0
            Else
                Return 1
            End If
        End Function
    End Class
    Friend Function GetATempDirectory() As String
        Dim localTempDir As String = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Process)
        Dim successed As Boolean = False
        Dim randomiser As New Random()
        Dim result As String = ""
        Do
            Dim buffer(2) As Byte
            randomiser.NextBytes(buffer)
            Dim folderPath As String = IO.Path.Combine(localTempDir, ("mcsrvmgr" & Hex(buffer(0)) & Hex(buffer(1)) & ".tmp"))
            If My.Computer.FileSystem.DirectoryExists(folderPath) Then
                successed = True
                My.Computer.FileSystem.CreateDirectory(folderPath)
                result = folderPath
            End If
        Loop Until successed
        Return result
    End Function
    Function GetSimpleVersionName(type As Server.EServerVersionType, Optional version As String = "") As String
        Select Case type
            Case Server.EServerVersionType.Vanilla
                Return "原版"
            Case Server.EServerVersionType.Forge
                Return "Forge"
            Case Server.EServerVersionType.Spigot
                Return "Spigot"
            Case Server.EServerVersionType.Spigot_Git
                Return "Spigot (Git)"
            Case Server.EServerVersionType.CraftBukkit
                Return "CraftBukkit"
            Case Server.EServerVersionType.SpongeVanilla
                Return "SpongeVanilla"
            Case Server.EServerVersionType.Paper
                Return "Paper"
            Case Server.EServerVersionType.Akarin
                Return "Akarin"
            Case Server.EServerVersionType.VanillaBedrock
                Return "原版(基岩專用)"
            Case Server.EServerVersionType.Nukkit
                Return "Nukkit"
            Case Server.EServerVersionType.Cauldron
                If version <> "" Then
                    If version = "1.5.2" OrElse version = "1.6.4" Then
                        Return "MCPC"
                    ElseIf version = "1.7.2" OrElse version = "1.7.10" Then
                        Return "Cauldron"
                    Else
                        Return "MCPC / Cauldron"
                    End If
                Else
                    Return "MCPC / Cauldron"
                End If
            Case Server.EServerVersionType.Thermos
                Return "Thermos"
            Case Else
                Return type.ToString
        End Select
    End Function
    Friend Function ReadAllText(path As String) As String
        If My.Computer.FileSystem.FileExists(path) Then
            Return My.Computer.FileSystem.ReadAllText(path)
        Else
            Return ""
        End If
    End Function
    Friend Sub WriteAllText(path As String, text As String)
        If My.Computer.FileSystem.FileExists(path) Then
            My.Computer.FileSystem.WriteAllText(path, text, False)
        Else
            Dim writer As New IO.StreamWriter(IO.File.Create(path))
            writer.Write(text)
            writer.Flush()
            writer.Close()
        End If
    End Sub

    Friend Function GetNukkitDownloadURL(NukkitVersionUrl As String) As String
        Dim manifestListURL As String = String.Format("{0}/api/json", NukkitVersionUrl)
        Dim client As New Net.WebClient()
        Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(client.DownloadString(manifestListURL))
        Dim result As String = (jsonObject.Item("artifacts").ToObject(Of JArray).First.ToObject(Of JObject).Item("relativePath").ToString)
        ' ci.nukkitx.com/job/NukkitX/job/master/178/artifact/target/nukkit-1.0-SNAPSHOT.jar
        Return NukkitVersionUrl & "/artifact/" & result
    End Function
    Friend Function GetUnicodedText(text As String) As String
        Dim header As String = "\u"
        Dim result As String = ""
        Dim regex As New Regex("[a-zA-z0-9]")
        For Each c As Char In text
            If regex.IsMatch(c) Then
                result &= c
            Else
                result &= (header & ToFourLength(Hex(AscW(c)).ToLower))
            End If
        Next

        Return result
    End Function
    Friend Function GetDeUnicodedText(text As String) As String
        ' "\u0000"
        Dim regex As New Regex("(\\u[0-9a-f]{4}|[a-zA-z0-9])")
        Dim result As String = ""
        For Each match As Match In regex.Matches(text)
            Try
                Select Case match.Value.Length
                    Case 6
                        result &= ChrW(Integer.Parse(match.Value.Substring(2), System.Globalization.NumberStyles.HexNumber))
                    Case 1
                        result &= match.Value
                End Select
            Catch ex As Exception
            End Try
        Next
        Return result
    End Function
    Sub NotifyInfoMessage(Message As String, Title As String)
        Manager.BeginInvoke(Sub() Manager.NotifyIcon1.ShowBalloonTip(5000, Title, Message, ToolTipIcon.Info))
    End Sub
    Friend Function TryGetFromDictionary(Of TKey, TValue)(dictionary As IDictionary(Of TKey, TValue), key As TKey, Optional defaultReturnValue As TValue = Nothing) As TValue
        If dictionary.ContainsKey(key) Then
            Return dictionary(key)
        Else
            Return defaultReturnValue
        End If
    End Function
    Private Function ToFourLength(input As String)

        Return input.PadLeft(4, "0")

    End Function
    Public Function TryGetKey(Server As Server, arg As String, Optional defaultString As String = "") As String
        Try
            Return Server.ServerOptions(arg)
        Catch ex As Exception
            Return defaultString
        End Try
    End Function
    Sub BeginInvokeIfRequired(control As Control, action As Action)
        If control.InvokeRequired Then
            control.BeginInvoke(action)
        Else
            action.Invoke()
        End If
    End Sub

End Module
