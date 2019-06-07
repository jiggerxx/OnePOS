Public Class inventory

    
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        showaddproduct()
        loadtypes()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        showaddstock()
        loadproducts()
        loadsuppliers()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        showeditprod()
        loadtypes()
        loadproducts()
    End Sub
End Class