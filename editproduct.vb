Public Class editproduct

    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        Dim unitword As String = ""
        Dim tandf As Boolean = False

        Try
            checkstate()
            dbconn.Open()

            With cmd
                .Connection = dbconn
                .CommandText = "SELECT * FROM products where prodname ='" & ComboBox4.Text & "'"
                dr = cmd.ExecuteReader

                While dr.Read

                    TextBox5.Text = dr.Item("prodcode")
                    TextBox3.Text = dr.Item("barcode")
                    TextBox2.Text = dr.Item("prodname")
                    TextBox4.Text = dr.Item("srp")
                    TextBox6.Text = dr.Item("prodtype")
                    'RichTextBox1.Text = dr.Item("proddesc")


                    unitword = dr.Item("unitword")


                End While

            End With
        Catch ex As Exception
            'MessageBox.Show(ex.Message + "Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        dbconn.Close()
        dbconn.Dispose()

        If tandf <> True Then
            'MessageBox.Show("Product does not exist!")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim prodcode As String = TextBox1.Text
        Dim tandf As Boolean = False
        Dim unitword As String = ""


        Try
            checkstate()
            dbconn.Open()

            With cmd
                .Connection = dbconn
                .CommandText = "SELECT * FROM products where barcode='" & prodcode & "'"
                dr = cmd.ExecuteReader

                While dr.Read

                    TextBox5.Text = dr.Item("prodcode")

                    TextBox3.Text = dr.Item("barcode")
                    TextBox2.Text = dr.Item("prodname")
                    TextBox4.Text = dr.Item("srp")
                    RichTextBox1.Text = dr.Item("proddesc")
                    TextBox6.Text = dr.Item("prodtype")
                    tandf = True


                End While

            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message + "Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        dbconn.Close()
        dbconn.Dispose()

        If tandf <> True Then
            MessageBox.Show("Product does not exist!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub
End Class