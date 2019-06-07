Public Class login

    'Private Sub Button2_Click(sender As Object, e As EventArgs)
    '    Me.Close()
    'End Sub
   
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim username As String = loginusername.Text
        Dim password As String = loginpassword.Text
        Dim active As Integer = 1
        Dim position As String

        If (String.IsNullOrEmpty(username) Or String.IsNullOrWhiteSpace(password)) Then
            MessageBox.Show("Please fill all fields!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else


            Dim wrapper As New EncryptDecryptVB(password)
            password = wrapper.EncryptData(password)


            Dim tandf As Boolean



            Try
                checkstate()
                dbconn.Open()

                With cmd
                    .Connection = dbconn
                    .CommandText = "SELECT * FROM user"
                    dr = cmd.ExecuteReader

                    While dr.Read
                        If username.Equals(dr.GetString("username")) And password.Equals(dr.GetString("password")) Then

                            Dim name As String = dr.Item("fname")
                            Dim mname As String = dr.Item("mname")
                            Dim lname As String = dr.Item("lname")
                            position = dr.Item("acctype")
                            MessageBox.Show("Welcome " + name + " " + mname + " " + lname + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            tandf = True






                            main.Show()
                            Me.Hide()
                        End If
                    End While
                    If tandf = False Then
                        MessageBox.Show("Account Not Found!", "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        loginpassword.Clear()
                        loginpassword.Focus()


                    End If

                    If position.Equals("CASHIER") Then
                        main.Button1.Visible = False
                        main.Button2.Visible = False
                        main.Button3.Visible = False
                        loadpos()
                    End If

                End With

            Catch ex As Exception
                MessageBox.Show(ex.Message + "Account Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                loginusername.Clear()
                loginpassword.Clear()
            End Try
        End If
        dbconn.Close()
        dbconn.Dispose()
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loginusername.Focus()
    End Sub
End Class