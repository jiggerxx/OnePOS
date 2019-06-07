Public Class addstock

    Dim counterX As Integer = 0
    Dim dataArray(100, 7) As String

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim total As Integer = 0
        Dim dbprodcode As String = ""
        Dim prodcode As String = ComboBox1.Text
        Dim exist As Boolean = False
        Dim added As Boolean = False
        Dim dealersprice As Double = TextBox7.Text
        Dim totalamountperproduct As Double = 0

        For x = 0 To counterX - 1
            If dataArray(x, 0) = prodcode(0) Then
                total = Convert.ToInt32(dataArray(x, 2)) + NumericUpDown1.Value
                dataArray(x, 2) = total.ToString
                totalamountperproduct = total * dealersprice
                dataArray(x, 6) = totalamountperproduct
                exist = True
            End If
        Next

        If exist.Equals(False) Then

            Try
                dbconn.Open()

                With cmd
                    .Connection = dbconn
                    .CommandText = "SELECT products.*,type.* FROM products LEFT JOIN type ON products.prodtype = type.typeID WHERE prodname = '" & prodcode & "'"
                    dr = cmd.ExecuteReader

                    While dr.Read

                        dataArray(counterX, 0) = dr.Item("prodcode")
                        dataArray(counterX, 1) = dr.Item("prodname")
                        dataArray(counterX, 2) = NumericUpDown1.Value
                        dataArray(counterX, 4) = dr.Item("prodtype")
                        dataArray(counterX, 5) = dealersprice
                        dataArray(counterX, 6) = CDbl(dealersprice) * NumericUpDown1.Value
                        dataArray(counterX, 7) = dr.Item("srp")
                        added = True

                    End While

                End With
            Catch ex As Exception
                MessageBox.Show(ex.Message + "Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            dbconn.Close()
            dbconn.Dispose()

        End If

        If added.Equals(True) Then
            counterX = counterX + 1
        End If

        DataGridView1.Rows.Clear()
        For x = 0 To counterX - 1
            DataGridView1.Rows.Add({dataArray(x, 0), dataArray(x, 1), dataArray(x, 2), dataArray(x, 4), dataArray(x, 5), dataArray(x, 7), Format(CDbl(dataArray(x, 6)), "0,0.00")})
        Next

        Button1.Enabled = True

    End Sub
End Class