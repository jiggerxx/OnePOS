Imports MySql.Data.MySqlClient
Imports System.Data.DataTable

Public Class pos

    Public barcode As String
    Public prodname As String
    Public price As String
    Public qty As Integer
    Public subtotal As String

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
      

        If TextBox1.Text.Count = 13 Then

            Dim amount As Double = 0
            Dim unitsx As Integer
            Dim barcode As String = TextBox1.Text
            Dim barcodex As String = ""

            Try

                dbconn.Open()

                With cmd

                    .Connection = dbconn
                    .CommandText = "SELECT * FROM products WHERE barcode = '" & barcode & "'"
                    dr = cmd.ExecuteReader

                    While dr.Read

                        barcodex = dr.Item("barcode")
                        prodname = dr.Item("prodname")
                        price = Format(CDbl(dr.Item("srp")), "0,0.00")
                        qty = 1

                    End While

                End With

            Catch ex As Exception

                MessageBox.Show(ex.Message + "Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

            dbconn.Close()
            dbconn.Dispose()

            subtotal = Format(CDbl(price) * qty, "0,0.00")

            DataGridView1.Rows.Insert(0, New String() {barcodex, prodname, price, qty, subtotal})
            DataGridView1.ClearSelection()

            checkduplicate()

            For indexs As Integer = 0 To DataGridView1.RowCount - 1
                amount += CDbl(DataGridView1.Rows(indexs).Cells(4).Value)
            Next

            Label4.Text = Format(amount, "0,0.00")

            For index As Integer = 0 To DataGridView1.RowCount - 1
                DataGridView1.Rows(index).DefaultCellStyle.ForeColor = Color.Green
                DataGridView1.Rows(index).DefaultCellStyle.Font = New Font("Courier New", 10)
            Next

            For index As Integer = 0 To DataGridView1.RowCount - 1
                unitsx += CDbl(DataGridView1.Rows(index).Cells(3).Value)
            Next

            Label6.Text = unitsx

            TextBox1.Clear()
            TextBox1.Focus()

        End If
    End Sub

    Sub checkduplicate()

        Dim amount As Double = 0
        Dim unitsx As Integer = 0

        For x As Integer = 0 To DataGridView1.Rows.Count - 1

            For y As Integer = 0 To DataGridView1.Rows.Count - 1

                If y <> x AndAlso DataGridView1.Rows(x).Cells(0).Value.ToString = DataGridView1.Rows(y).Cells(0).Value.ToString Then

                    DataGridView1.Rows(x).Cells(3).Value = CInt(DataGridView1.Rows(y).Cells(3).Value) + qty
                    DataGridView1.Rows(x).Cells(4).Value = Format(CDbl(DataGridView1.Rows(x).Cells(4).Value) * CDbl(DataGridView1.Rows(x).Cells(3).Value), "0,0.00")
                    DataGridView1.Rows.Remove(DataGridView1.Rows(y))
                    DataGridView1.ClearSelection()

                    For index As Integer = 0 To DataGridView1.RowCount - 1

                        DataGridView1.Rows(index).DefaultCellStyle.ForeColor = Color.Green
                        DataGridView1.Rows(index).DefaultCellStyle.Font = New Font("Courier New", 10)

                    Next

                    TextBox1.Clear()
                    TextBox1.Focus()

                    Exit Sub
                End If
            Next
        Next

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.F1 Then
            If DataGridView1.RowCount <> 0 Then
                invoice.Show()
            Else
                MessageBox.Show("Please add item(s) to transaction", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        ElseIf e.KeyCode = Keys.Escape Then

            loadpos()
            loadposnum()

        End If
    End Sub
End Class