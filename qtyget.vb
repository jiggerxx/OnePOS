Public Class qtyget

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            pos.subtotal = pos.price * CInt(TextBox1.Text)
            Me.Close()
        End If
    End Sub

    Private Sub qtyget_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub
End Class