Imports System
Imports System.Data
Imports MySql.Data.MySqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class invoice
    Private Sub invoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Transaction #:" & pos.TextBox2.Text
        TextBox2.Focus()
        TextBox1.Text = pos.Label4.Text

    End Sub
    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        If (TextBox2.Text.Equals("")) Then

            TextBox2.Text = ""
            TextBox2.BackColor = Color.White
            TextBox2.ForeColor = Color.Black

        ElseIf CDbl(TextBox1.Text) < CDbl(TextBox2.Text) Or CDbl(TextBox1.Text) = CDbl(TextBox2.Text) Then

            TextBox2.BackColor = Color.Green
            TextBox2.ForeColor = Color.White

        Else

            TextBox2.BackColor = Color.Red
            TextBox2.ForeColor = Color.White

        End If

    End Sub
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown

        If e.KeyCode = Keys.Enter Then

            TextBox3.Text = Format((CDbl(TextBox2.Text) - CDbl(TextBox1.Text)), "0,0.00")
            TextBox2.Text = Format(CDbl(TextBox2.Text), "0,0.00")

            If TextBox2.BackColor = Color.Red Then

                MsgBox("Insufficient Amount.")

            End If
            TextBox2.Focus()
        End If

        If e.KeyCode = Keys.F2 And TextBox2.BackColor = Color.Green Then

            Dim transnumber As String = pos.TextBox2.Text
            Dim transacsnum As String
            Dim datetransac As String = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")
            Dim totalcost As Double = Convert.ToDouble(TextBox1.Text)
            Dim cashier As String = main.Label1.Text
            Dim cash As Double = CDbl(TextBox2.Text)
            Dim change As Double = CDbl(TextBox3.Text)
            Dim units As Integer = CInt(pos.Label6.Text)

            Try

                checkstate()
                dbconn.Close()
                dbconn.Open()
                With cmd
                    .Connection = dbconn
                    .CommandText = "INSERT INTO resibo VALUES('" & transnumber & "','" & datetransac & "','" & totalcost & "','" & cashier & "', '" & cash & "', '" & change & "')"
                    .ExecuteNonQuery()
                    MessageBox.Show("Transaction #" + transnumber + " has been added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    transacsnum = transnumber
                    TextBox1.Clear()
                    TextBox3.Clear()
                End With
            Catch ex As Exception
                MessageBox.Show("Transaction #" + transnumber + " not added! " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            dbconn.Close()
            dbconn.Dispose()


            For x As Integer = 0 To pos.DataGridView1.RowCount - 1

                Dim stocks As Integer = 0
                Try

                    checkstate()
                    dbconn.Open()
                    With cmd
                        .Connection = dbconn
                        .CommandText = "insert into resibo_products values('" & transnumber & "','" & pos.DataGridView1.Rows(x).Cells(0).Value & "','" & pos.DataGridView1.Rows(x).Cells(3).Value & "','" & pos.DataGridView1.Rows(x).Cells(4).Value & "')"
                        .ExecuteNonQuery()

                    End With
                Catch ex As Exception
                    MessageBox.Show("error! 123" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try
                dbconn.Close()
                dbconn.Dispose()

                Try
                    checkstate()
                    dbconn.Open()

                    With cmd
                        .Connection = dbconn
                        .CommandText = "SELECT stock FROM products WHERE prodcode = '" & pos.DataGridView1.Rows(x).Cells(0).Value & "'"
                        dr = cmd.ExecuteReader

                        While dr.Read
                            stocks = dr.Item("stock")
                        End While

                    End With

                Catch ex As Exception
                    MessageBox.Show(ex.Message + "error!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                dbconn.Close()
                dbconn.Dispose()

                stocks = stocks - pos.DataGridView1.Rows(x).Cells(3).Value

                Try
                    checkstate()
                    dbconn.Open()
                    With cmd
                        .Connection = dbconn
                        .CommandText = "update products set stock='" & stocks & "' where prodcode='" & pos.DataGridView1.Rows(x).Cells(0).Value & "'"
                        .ExecuteNonQuery()

                    End With
                Catch ex As Exception
                    MessageBox.Show("error!" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)

                End Try
                dbconn.Close()
                dbconn.Dispose()


            Next


            dbconn.Close()
            dbconn.Dispose()

            printreceipt.datafrom = "invoice"
            printreceipt.selectedtransacnum = transnumber
            printreceipt.ShowDialog()

            'Dim dsDA As New MySqlDataAdapter
            'Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'Try

            '    Dim resibo As New DataSet

            '    dbconn.Open()

            '    Dim strSQL As String = ""
            '    strSQL = "SELECT CONCAT(products.prodname) as articles, resibo.*, resibo_products.*, products.* FROM resibo LEFT JOIN resibo_products ON resibo.transacnum = resibo_products.transacnum LEFT JOIN products ON resibo_products.prodcode = products.prodcode  WHERE resibo.transacnum='" & transacsnum & "'"
            '    dsDA.SelectCommand = New MySqlCommand(strSQL, dbconn)
            '    dsDA.Fill(resibo, "resibo")

            '    rd = New resibo
            '    rd.SetDataSource(resibo)

            '    reportviewer.CrystalReportViewer1.ReportSource = rd
            '    reportviewer.ShowDialog()
            '    reportviewer.Dispose()

            '    dbconn.Close()
            '    dbconn.Dispose()



            'Catch ex As Exception
            '    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'End Try

            ''Me.TextBox1.Text = ""
            ''Me.TextBox2.Text = ""
            ''Me.TextBox3.Text = ""

            loadpos()
            loadposnum()

            Me.Close()
        End If

    End Sub
End Class