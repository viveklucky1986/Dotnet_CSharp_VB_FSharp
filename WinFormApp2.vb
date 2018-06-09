Imports System.Drawing
Imports System.Windows.Forms

Namespace MyNamespace
    Public Class MyFormClass
    Inherits Form
    private singleInstance as boolean = false
    private hideCommandlineWindow as boolean = false

' this will be used later for single instance mode
    Private objMutex As System.Threading.Mutex

' this will be used later to hide the command line window
    Private Declare Auto Function GetConsoleWindow Lib "kernel32.dll" () As IntPtr
    Private Declare Auto Function ShowWindow Lib "user32.dll" _
         (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean
    Private Const     SW_HIDE    as Integer =     0
    Private Const     SW_SHOW as Integer =     5
    private hWndConsole As Integer

    Private XPreviousClick, YPreviousClick as integer

' declare variables for your controls
    Private tmr1 As Timer
    Private btn1, btn2 As Button
    Private lbl1 as Label
    Private txb1 as textbox

    Public Sub New()
' single instance mode
        if singleinstance then  'Prevent opening second instance if desired
            objMutex = New System.Threading.Mutex(False, "Name of your program")
            If objMutex.WaitOne(0, False) = False Then
                objMutex.Close()
                objMutex = Nothing
                MessageBox.Show("Another instance is already running!")
                End
            End If
        End if

' hide the command line window
        hWndConsole = GetConsoleWindow() 'get the handle of commandline window
        if hideCommandlineWindow then
            If hWndConsole <> IntPtr.Zero Then ShowWindow(hWndConsole, SW_HIDE)
        End If

        DisplayTheForm()
    End Sub

Private Sub DisplayTheForm()
    Me.Name = "Windows Form" '
    Me.Text = "Here's your windows header text."
    Me.Size = New Size(706, 250)
    Me.StartPosition = FormStartPosition.CenterScreen

'create your controls and define their Event handlers
    'timer
    tmr1 = new timer()
    AddHandler tmr1.Tick, AddressOf Tmr1Tick

    'buttons
    btn1 = New Button()
    btn1.Name = "button"
    btn1.Text = "Hide"
    btn1.Location = New Point(10, 5)
    btn1.Size = New Size(80, 35)
    Me.Controls.Add(btn1)
    AddHandler btn1.Click, AddressOf btn1Click

    btn2 = New Button()
    btn2.Name = "button"
    btn2.Text = "Start timer1"
    btn2.Location = New Point(300, 5)
    btn2.Size = New Size(80, 35)
    me.controls.add(btn2)
    AddHandler btn2.Click, AddressOf btn2Click

    lbl1  = new Label()
    lbl1.text = "Left-click on left top corner of intended control to see its coordinates" & vbcrlf & "Right-click on right bottom corner of intended control to see it's size."
    lbl1.Location = New Point(11, 45)
    lbl1.Size = New Size(370, 37)
    me.controls.add(lbl1 )
  txb1  = New textbox()
    txb1.text = " "
    txb1.Location = New Point(95, 6)
    txb1.Size = New Size(200, 25)
    me.controls.add(txb1)

    'an example of froms event handler
    Addhandler me.mousedown ,Addressof MeMousedown
End Sub

' the actual event handler subroutines

'close the application when the form closes
Private Sub FormcloseClick(source As Object, e As EventArgs)
    '  to close your application when the form closes
    application.exit()
    end
End Sub

Private Sub Tmr1Tick(source As Object, e As EventArgs)
    tmr1.stop()
    MessageBox.Show("Timer 1 has elapsed !")
End Sub

' Toggle visibility of command line window usigh previously stored window handle
Private Sub btn1Click(source As Object, e As EventArgs)
    if btn1.text = "Hide" then
        ShowWindow(hWndConsole, SW_HIDE)
        btn1.text = "Show"
    else
        ShowWindow(hWndConsole, SW_SHOW)
        btn1.text = "Hide"
    end if
End Sub

Private Sub btn2Click(source As Object, e As EventArgs)
    tmr1.interval = 5000  ' 5 seconds
    tmr1.start()
End Sub

Private Sub MeMousedown(source As Object, e As MouseEventArgs)
    If (e.Button = MouseButtons.Left) then
        lbl1 .text = "New Point(" & convert.tostring(e.x) & "," &  convert.tostring(e.y) & ")"
        XPreviousClick = e.x
        YPreviousClick = e.y
    end if
    If (e.Button = MouseButtons.Right) then
        lbl1 .text = "New Size(" & convert.tostring(e.x - XPreviousClick ) & "," & convert.tostring(e.y - YPreviousClick ) & ")"
    end if
    txb1.text = lbl1.text
End Sub

        Public Shared Sub Main(args As [String]())
            'Create an instance of the form and show it
            Application.Run(New MyFormClass())
        End Sub

    End Class
End Namespace