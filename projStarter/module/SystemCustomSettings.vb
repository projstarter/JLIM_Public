Imports System.IO
Imports System.Web.Script.Serialization

Module SystemCustomSettings
    Private ReadOnly jsonPath As String = IO.Path.Combine(Application.StartupPath, "System.json")

    Private settingsData As Object
    Private ReadOnly serializer As New JavaScriptSerializer()


    ' Load JSON into memory
    Public Sub LoadSettings()
        If Not File.Exists(jsonPath) Then
            Throw New FileNotFoundException("JSON file not found", jsonPath)
        End If

        Dim jsonText As String = File.ReadAllText(jsonPath).Trim()

        If String.IsNullOrWhiteSpace(jsonText) Then
            Throw New Exception("JSON file is empty.")
        End If

        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim settingsData =
            serializer.Deserialize(Of Dictionary(Of String, Object))(jsonText)



    End Sub

    ' Get a value by path: "settings.application.description.value"
    Public Function GetValue(path As String) As String
        Dim parts() As String = path.Split("."c)
        Dim current As Object = settingsData

        For Each part In parts
            If TypeOf current Is Dictionary(Of String, Object) Then
                Dim dict = CType(current, Dictionary(Of String, Object))

                If dict.ContainsKey(part) Then
                    current = dict(part)
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Next

        Return current?.ToString()
    End Function
    ' Update a value and save JSON file
    Public Sub SetValue(path As String, newValue As String)
        Dim parts() As String = path.Split("."c)
        Dim current As Object = settingsData

        For i As Integer = 0 To parts.Length - 2
            If TypeOf current Is Dictionary(Of String, Object) Then
                Dim dict As Dictionary(Of String, Object) = CType(current, Dictionary(Of String, Object))
                If dict.ContainsKey(parts(i)) Then
                    current = dict(parts(i))
                Else
                    Dim newDict As New Dictionary(Of String, Object)
                    dict(parts(i)) = newDict
                    current = newDict
                End If
            Else
                Throw New Exception("Invalid path.")
            End If
        Next

        Dim finalDict As Dictionary(Of String, Object) = CType(current, Dictionary(Of String, Object))
        finalDict(parts(parts.Length - 1)) = newValue

        ' Save back to JSON file
        Dim updatedJson As String = serializer.Serialize(settingsData)
        File.WriteAllText(jsonPath, updatedJson)
    End Sub
End Module
