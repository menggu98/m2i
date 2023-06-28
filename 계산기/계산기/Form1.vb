Public Class Form1
    Dim calcmode As Integer = 0 '계산형태 ex) 더하기 빼기 곱하기 나누기
    ' 0 이면 계산실행을 안했다는 의미
    ' 1 더하기
    ' 2 빼기
    ' 3 곱하기
    ' 4 나누기
    Dim prev As String = "" ' 이전 실행했던 값을 의미
    Dim caldone As Boolean = False ' 계산완료 초기값 False
    Dim clearlog As Boolean = False ' 계산 후 기록 삭제용
    Private Sub B1_Click(sender As Object, e As EventArgs) Handles B1.Click, B2.Click, B3.Click,
        B4.Click, B5.Click, B6.Click, B7.Click, B8.Click, B9.Click, B0.Click, Bdot.Click
        addvalue(sender.Text)
    End Sub

    Private Sub Bresult_Click(sender As Object, e As EventArgs) Handles Bresult.Click
        runcal(True)
    End Sub

    Private Sub Bplus_Click(sender As Object, e As EventArgs) Handles Bplus.Click, Bminus.Click,
            Bmulti.Click, Bslash.Click
        Select Case sender.Name
            Case Bplus.Name
                setcalcmode(1)
            Case Bminus.Name
                setcalcmode(2)
            Case Bmulti.Name
                setcalcmode(3)
            Case Bslash.Name
                setcalcmode(4)
        End Select
    End Sub
    Private Sub addvalue(value As String)
        If Not calcmode = 0 Then
            If prev = "" Then
                prev = TextBox1.Text
                TextBox1.Text = ""

            End If
        End If
        If caldone Then
            TextBox1.Text = ""
            caldone = False
        End If
        If clearlog Then
            TextBox2.Text = ""
            clearlog = False
        End If

        TextBox1.Text += value
    End Sub


    Private Sub Bclear_Click(sender As Object, e As EventArgs) Handles Bclear.Click

        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Bdelete_Click(sender As Object, e As EventArgs) Handles Bdelete.Click
        TextBox1.Text = "" '지우기 버튼 클릭 시
    End Sub

    Private Sub Bplusminus_Click(sender As Object, e As EventArgs) Handles Bplusminus.Click
        If IsNumeric(TextBox1.Text) Then
            TextBox1.Text = (Convert.ToDouble(TextBox1.Text) * -1).ToString
            ' convert.todouble 메서드로 부동소수점까지 표현
            ' -1을 곱하여 음수로 표기하도록
            ' tostring 문자열형태로
        End If
    End Sub

    Private Sub setcalcmode(mode As Integer)
        calcmode = mode
        Bplus.ForeColor = Color.FromArgb(186, 206, 227) ' 푸르스름한 색으로 표현
        Bminus.ForeColor = Color.FromArgb(186, 206, 227)
        Bmulti.ForeColor = Color.FromArgb(186, 206, 227)
        Bslash.ForeColor = Color.FromArgb(186, 206, 227)

        Select Case mode
            Case 1
                Bplus.ForeColor = Color.SkyBlue
            Case 2
                Bplus.ForeColor = Color.SkyBlue
            Case 3
                Bplus.ForeColor = Color.SkyBlue
            Case 4
                Bplus.ForeColor = Color.SkyBlue
        End Select
    End Sub

    Private Function calculator(num1 As String, num2 As String)
        Dim a As Double = Convert.ToDouble(num1) ' 변수 a 선언하고 num1을 실수로 변환하고 저장
        Dim b As Double = Convert.ToDouble(num2)

        Select Case calcmode
            Case 1
                Return a + b ' 함수기에 반환값을 설정
            Case 2
                Return a - b
            Case 3
                Return a * b
            Case 4
                Return a / b

        End Select
    End Function

    Private Sub runcal(fin As Boolean)
        If prev = "" Then Exit Sub ' 이전 값이 없다면 프로시저 종료

        Try
            If TextBox2.Text = "" Then update(prev, True)
            update(TextBox1.Text, False)
            TextBox1.Text = calculator(prev, TextBox1.Text)

                If fin Then
                    clearlog = True
                    TextBox2.Text += " ="

                End If

         Catch ex As Exception
            TextBox1.Text = "오류"
        End Try
        resetcal()
    End Sub

    Private Sub update(plusval As String, numone As Boolean)
        'TextBox2.Text += prev + " "

        If Not numone Then
            Select Case calcmode
                Case 1
                    TextBox2.Text += " + "
                Case 2
                    TextBox2.Text += " - "
                Case 3
                    TextBox2.Text += " x "
                Case 4
                    TextBox2.Text += " ÷ "
            End Select

        End If
        TextBox2.Text += plusval
    End Sub

    Private Sub resetcal()
        setcalcmode(0)
        prev = ""
        caldone = True
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Bresult.Focus()

        Select Case e.KeyCode
            Case Keys.D1, Keys.NumPad1
                addvalue("1")
            Case Keys.D2, Keys.NumPad2
                addvalue("2")
            Case Keys.D3, Keys.NumPad3
                addvalue("3")
            Case Keys.D4, Keys.NumPad4
                addvalue("4")
            Case Keys.D5, Keys.NumPad5
                addvalue("5")
            Case Keys.D6, Keys.NumPad6
                addvalue("6")
            Case Keys.D7, Keys.NumPad7
                addvalue("7")
            Case Keys.D8, Keys.NumPad8
                addvalue("8")
            Case Keys.D9, Keys.NumPad9
                addvalue("9")
            Case Keys.D0, Keys.NumPad0
                addvalue("0")
            Case Keys.Decimal, Keys.OemPeriod
                addvalue(".")
            Case Keys.Enter
                runcal(True)
            Case Keys.Back
                TextBox1.Text = Mid(TextBox1.Text, 1, TextBox1.Text.Length - 1)
            Case Keys.Add, Keys.OemMinus, Keys.Subtract, Keys.Multiply, Keys.Divide
                If Not calcmode = 0 And Not prev = "" Then runcal(False)

                Select Case e.KeyCode
                    Case Keys.Add
                        setcalcmode(1)
                    Case Keys.OemMinus, Keys.Subtract
                        setcalcmode(2)
                    Case Keys.Multiply
                        setcalcmode(3)
                    Case Keys.Divide
                        setcalcmode(4)
                End Select
            Case Keys.Escape
                resetcal()
                TextBox1.Text = ""
                TextBox2.Text = ""
            Case Keys.Delete
                TextBox1.Text = ""
        End Select
    End Sub
End Class
