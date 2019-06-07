Imports System
Imports System.Data
Imports MySql.Data.MySqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class printreceipt

    Public datafrom As String
    Public selectedtransacnum As String

    Private conn As String = "Data Source=localhost; Database=oneposdb; User ID=root; Password=;"

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dsConn As New MySqlConnection(conn)
        Dim dsDA As New MySqlDataAdapter
        Dim rd As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        
        Try

            Dim resibo As New DataSet

            dsConn.Open()

            Dim strSQL As String = ""

            strSQL = " SELECT FORMAT(products.srp,2) as srp,FORMAT(resibo.cash,2) as cash,FORMAT(resibo.changex,2) as changex ,CONCAT(products.prodname) as articles,resibo.*,resibo_products.*,products.* FROM resibo LEFT JOIN resibo_products ON resibo.transacnum = resibo_products.transacnum LEFT JOIN products ON resibo_products.prodcode = products.barcode WHERE resibo.transacnum = '" & selectedtransacnum & "'"
            dsDA.SelectCommand = New MySqlCommand(strSQL, dsConn)
            dsDA.Fill(resibo, "resibo")

            rd = New resibo
            rd.SetDataSource(resibo)

            ReportView.CrystalReportViewer1.ReportSource = rd
            ReportView.ShowDialog()
            ReportView.Dispose()

            dsConn.Close()
            dsConn.Dispose()

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()
        invoice.Close()
        loadpos()
        loadposnum()

    End Sub
End Class