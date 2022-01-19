
interface IHandler
{
    public event AxisHandler OnHorizontalAxis;
    public event AxisHandler OnVerticalAxis;
    public event AxisHandler OnVerticatMouseAxis;
    public event AxisHandler OnHorizontalMouseAxis;
    public event ButtonHandler OnJump;
}
