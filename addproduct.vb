Public Class addproduct

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        addtype.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim prodname As String = TextBox2.Text
        Dim barcode As String = TextBox3.Text
        Dim unit As String
        Dim unitvalue As String
        Dim prodtype As String
        Dim prodvalue As String
        Dim srp As Double = TextBox4.Text

        Dim result As Integer = MessageBox.Show("Add item?" + vbNewLine + "Prodcut Name: " + prodname.ToUpper, "", MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Cancel Then

        ElseIf result = DialogResult.No Then

        ElseIf result = DialogResult.Yes Then

            Try
                prodtype = DirectCast(ComboBox3.SelectedItem, KeyValuePair(Of String, String)).Key
                prodvalue = DirectCast(ComboBox3.SelectedItem, KeyValuePair(Of String, String)).Value
            Catch ex As Exception
                unit = ""
                unitvalue = ""
                prodtype = ""
                prodvalue = ""
            End Try


            Try

                checkstate()
                dbconn.Open()
                With cmd
                    .Connection = dbconn
                    .CommandText = "INSERT INTO products (barcode,prodname,prodtype,stock,srp) VALUES('" & barcode & "','" & prodname & "','" & prodtype & "',0,'" & srp & "')"
                    .ExecuteNonQuery()
                    MessageBox.Show("Product " + prodname + " has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TextBox2.Clear()
                    TextBox3.Clear()
                    ComboBox3.SelectedIndex = -1
                    TextBox4.Text = 0.0

                End With
            Catch ex As Exception
                MessageBox.Show("Product Not Added! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

            dbconn.Close()
            dbconn.Dispose()
        End If
    End Sub
End Class