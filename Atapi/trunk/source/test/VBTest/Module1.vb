Imports JulMar.Atapi

Module Module1

    Dim WithEvents myTAPI As JulMar.Atapi.TapiManager

    Sub Main()

        myTAPI = New TapiManager("Phone.Net")

        If Not myTAPI.Initialize Then
            Console.WriteLine("TAPI-Initialize failed")
            Exit Sub
        Else

            Dim lineArray As TapiLine() = myTAPI.Lines
            Dim i As Integer = 0
            Dim x As String


            For i = 0 To lineArray.Length - 1
                Dim line As TapiLine = lineArray(i)
                If i = 0 Then
                    x = "OPEN.....: "
                    line.Monitor()
                Else
                    x = "NOT OPEN.: "
                End If

                Console.WriteLine(x & line.Name)

            Next

        End If

        '--- Wait here ---
        Console.ReadLine()

        '--- Exit TAPI ---
        myTAPI.Shutdown()

    End Sub

    Private Sub OnNewCall(ByVal sender As Object, ByVal e As NewCallEventArgs) Handles myTAPI.NewCall

        Console.WriteLine("(New-Call) Caller: " & e.Call.CallerId & " | Called: " & e.Call.CalledId)
        e.Call.Answer()

    End Sub

    Private Sub OnCallInfoChange(ByVal sender As Object, ByVal e As CallInfoChangeEventArgs) Handles myTAPI.CallInfoChanged
        Console.WriteLine("CallInfo: " & e.Change.ToString() & " - " & e.Call.ToString())
    End Sub

End Module
