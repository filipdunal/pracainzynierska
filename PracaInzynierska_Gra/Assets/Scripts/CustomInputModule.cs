using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
#pragma warning disable 618
public class CustomInputModule : PointerInputModule
{
    /*
    [HideInInspector] public static CustomInputModule instance;
    
    protected new void Awake()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log(gameObject.name);
            Destroy(gameObject);
        }
    }
    */

    private float m_NextAction;

    private Vector2 m_LastMousePosition;
    private Vector2 m_MousePosition;

    protected CustomInputModule() { }

    [Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
    public enum InputMode
    {
        Mouse,
        Buttons
    }

    [Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
    public InputMode inputMode
    {
        get { return InputMode.Mouse; }
    }

    [SerializeField]
    private string m_HorizontalAxis = "Horizontal";

    /// <summary>
    /// Name of the vertical axis for movement (if axis events are used).
    /// </summary>
    [SerializeField]
    private string m_VerticalAxis = "Vertical";

    /// <summary>
    /// Name of the submit button.
    /// </summary>
    [SerializeField]
    private string m_SubmitButton = "Submit";

    /// <summary>
    /// Name of the submit button.
    /// </summary>
    [SerializeField]
    private string m_CancelButton = "Cancel";

    [SerializeField]
    private float m_InputActionsPerSecond = 10;

    [SerializeField]
    private bool m_AllowActivationOnMobileDevice;

    public bool allowActivationOnMobileDevice
    {
        get { return m_AllowActivationOnMobileDevice; }
        set { m_AllowActivationOnMobileDevice = value; }
    }

    public float inputActionsPerSecond
    {
        get { return m_InputActionsPerSecond; }
        set { m_InputActionsPerSecond = value; }
    }

    /// <summary>
    /// Name of the horizontal axis for movement (if axis events are used).
    /// </summary>
    public string horizontalAxis
    {
        get { return m_HorizontalAxis; }
        set { m_HorizontalAxis = value; }
    }

    /// <summary>
    /// Name of the vertical axis for movement (if axis events are used).
    /// </summary>
    public string verticalAxis
    {
        get { return m_VerticalAxis; }
        set { m_VerticalAxis = value; }
    }

    public string submitButton
    {
        get { return m_SubmitButton; }
        set { m_SubmitButton = value; }
    }

    public string cancelButton
    {
        get { return m_CancelButton; }
        set { m_CancelButton = value; }
    }

    public Texture2D cursor;
    public Vector2 cursorPosition;
    public static Vector2 mousePos;
    [HideInInspector] public static bool visible = true;
    protected override void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public enum Controller
    {
        phone,
        mouse,
        gamepad
    }
    public Controller controller;
    private void SwitchController()
    {
        controller++;
        if(!Enum.IsDefined(typeof(Controller),controller))
        {
            controller = 0;
        }
    }
    [SerializeField] GameObject objectBelowPointer;

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);
    private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
    private const uint MOUSEEVENTF_LEFTUP = 0x04;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
    private const uint MOUSEEVENTF_RIGHTUP = 0x10;

    private void Update()
    {
        if(Input.GetButtonDown("Switch Input Mode"))
        {
            SwitchController();
        }
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (visible)
            {
                if (controller == Controller.mouse)
                {
                    cursorPosition += new Vector2(Input.GetAxisRaw("Mouse X") * 20, -Input.GetAxisRaw("Mouse Y") * 20);
                }
                else if(controller==Controller.phone)
                {
                    cursorPosition = gameObject.GetComponent<AxisSteeringScript>().mousePosition;
                }
                else if(controller==Controller.gamepad)
                {
                    cursorPosition += new Vector2(Input.GetAxisRaw("Horizontal") * 20, -Input.GetAxisRaw("Vertical") * 20);
                    if(Input.GetButton("Fire2"))
                    {
                        SendMouseLeftClick();
                    }
                    if(Input.GetButtonUp("Fire2"))
                    {
                        ReleaseMouseLeftClick();
                    }
                }
                //mousePos = new Vector2(cursorPosition.x, cursorPosition.y);

                mousePos = new Vector2(cursorPosition.x, (cursorPosition.y - Screen.height) * -1);
            }
        }
        else
            ShowCursor(false);
    }
    
    public void SendMouseLeftClick()
    {
        if (Application.isFocused) //Probably not needed 'if' statement
        {
            Vector2 p = new Vector2(Display.main.systemWidth, Display.main.systemHeight);
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, (UIntPtr)0); DELETE THIS
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)p.x, (uint)p.y, 0, (UIntPtr)0);
            
        }
    }
    //[SerializeField] float durationOfFakeClick = 0.1f; DELETE THIS
    public void ReleaseMouseLeftClick()
    {
        if(Application.isFocused)
        {
            Vector2 p = new Vector2(Display.main.systemWidth, Display.main.systemHeight);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)p.x, (uint)p.y, 0, (UIntPtr)0);
        }
    }

    public void ShowCursor(bool show)
    {
        visible = show;
    }

    [Header("Cursor image settings")]
    private int pixelSizeOfCursor = 1;
    [SerializeField]
    private float xOffsetOfCursor = 0.5f;
    [SerializeField]
    private float yOffsetOfCursor = 0.5f;
    [SerializeField]
    private float scaleOfCursor = 100f;


    private void OnGUI()
    {
        scaleOfCursor = ((Screen.width + Screen.height) / 2f)/20f;
        if (cursorPosition.x < 0)
            cursorPosition = new Vector2(0, cursorPosition.y);
        if (cursorPosition.x > Screen.width)
            cursorPosition = new Vector2(Screen.width, cursorPosition.y);
        if (cursorPosition.y < 0)
            cursorPosition = new Vector2(cursorPosition.x, 0);
        if (cursorPosition.y > Screen.height)
            cursorPosition = new Vector2(cursorPosition.x, Screen.height);
        if (visible)
        {
            GUI.DrawTexture(new Rect(cursorPosition.x - (xOffsetOfCursor * scaleOfCursor), cursorPosition.y - (yOffsetOfCursor * scaleOfCursor), pixelSizeOfCursor * scaleOfCursor, pixelSizeOfCursor * scaleOfCursor), cursor, ScaleMode.ScaleAndCrop, true, 0);
        }

    }

    public override void UpdateModule()
    {
        m_LastMousePosition = m_MousePosition;
        m_MousePosition = new Vector2(cursorPosition.x, (cursorPosition.y - Screen.height) * -1);
    }

    public override bool IsModuleSupported()
    {
        // Check for mouse presence instead of whether touch is supported,
        // as you can connect mouse to a tablet and in that case we'd want
        // to use StandaloneInputModule for non-touch input events.
        return m_AllowActivationOnMobileDevice || Input.mousePresent;
    }

    public override bool ShouldActivateModule()
    {
        if (!base.ShouldActivateModule())
            return false;

        var shouldActivate = Input.GetButtonDown(m_SubmitButton);
        shouldActivate |= Input.GetButtonDown(m_CancelButton);
        shouldActivate |= !Mathf.Approximately(Input.GetAxisRaw(m_HorizontalAxis), 0.0f);
        shouldActivate |= !Mathf.Approximately(Input.GetAxisRaw(m_VerticalAxis), 0.0f);
        shouldActivate |= (m_MousePosition - m_LastMousePosition).sqrMagnitude > 0.0f;
        shouldActivate |= Input.GetMouseButtonDown(0);
        return shouldActivate;
    }

    public override void ActivateModule()
    {
        base.ActivateModule();
        m_MousePosition = mousePos;
        m_LastMousePosition = mousePos;

        var toSelect = eventSystem.currentSelectedGameObject;
        if (toSelect == null)
            toSelect = eventSystem.lastSelectedGameObject;
        if (toSelect == null)
            toSelect = eventSystem.firstSelectedGameObject;

        eventSystem.SetSelectedGameObject(toSelect, GetBaseEventData());
    }

    public override void DeactivateModule()
    {
        base.DeactivateModule();
        ClearSelection();
    }

    public override void Process()
    {
        bool usedEvent = SendUpdateEventToSelectedObject();

        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();

            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }

        ProcessMouseEvent();
    }

    /// <summary>
    /// Process submit keys.
    /// </summary>
    private bool SendSubmitEventToSelectedObject()
    {
        if (eventSystem.currentSelectedGameObject == null)
            return false;

        var data = GetBaseEventData();
        if (Input.GetButtonDown(m_SubmitButton))
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.submitHandler);

        if (Input.GetButtonDown(m_CancelButton))
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.cancelHandler);
        return data.used;
    }

    private bool AllowMoveEventProcessing(float time)
    {
        bool allow = Input.GetButtonDown(m_HorizontalAxis);
        allow |= Input.GetButtonDown(m_VerticalAxis);
        allow |= (time > m_NextAction);
        return allow;
    }

    private Vector2 GetRawMoveVector()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxisRaw(m_HorizontalAxis);
        move.y = Input.GetAxisRaw(m_VerticalAxis);
        //print(move.x);

        if (Input.GetButtonDown(m_HorizontalAxis))
        {
            if (move.x < 0)
                move.x = -1f;
            if (move.x > 0)
                move.x = 1f;
        }
        if (Input.GetButtonDown(m_VerticalAxis))
        {
            if (move.y < 0)
                move.y = -1f;
            if (move.y > 0)
                move.y = 1f;
        }
        return move;
    }

    /// <summary>
    /// Process keyboard events.
    /// </summary>
    private bool SendMoveEventToSelectedObject()
    {
        float time = Time.unscaledTime;

        if (!AllowMoveEventProcessing(time))
            return false;

        Vector2 movement = GetRawMoveVector();
        // Debug.Log(m_ProcessingEvent.rawType + " axis:" + m_AllowAxisEvents + " value:" + "(" + x + "," + y + ")");
        var axisEventData = GetAxisEventData(movement.x, movement.y, 0.6f);
        if (!Mathf.Approximately(axisEventData.moveVector.x, 0f) ||
            !Mathf.Approximately(axisEventData.moveVector.y, 0f))
        {
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
        }
        m_NextAction = time + 1f / m_InputActionsPerSecond;
        return axisEventData.used;
    }

    public new const int kMouseLeftId = -1;
    public new const int kMouseRightId = -2;
    public new const int kMouseMiddleId = -3;

    private readonly MouseState m_MouseState = new MouseState();
    protected new virtual MouseState GetMousePointerEventData()
    {
        // Populate the left button...
        PointerEventData leftData;
        var created = GetPointerData(kMouseLeftId, out leftData, true);

        leftData.Reset();

        if (created)
            leftData.position = m_MousePosition;

        Vector2 pos = m_MousePosition;
        leftData.delta = pos - leftData.position;
        leftData.position = pos;
        leftData.scrollDelta = Input.mouseScrollDelta;
        leftData.button = PointerEventData.InputButton.Left;
        eventSystem.RaycastAll(leftData, m_RaycastResultCache);
        var raycast = FindFirstRaycast(m_RaycastResultCache);

        leftData.pointerCurrentRaycast = raycast;
        m_RaycastResultCache.Clear();

        // copy the apropriate data into right and middle slots
        PointerEventData rightData;
        GetPointerData(kMouseRightId, out rightData, true);
        CopyFromTo(leftData, rightData);
        rightData.button = PointerEventData.InputButton.Right;

        PointerEventData middleData;
        GetPointerData(kMouseMiddleId, out middleData, true);
        CopyFromTo(leftData, middleData);
        middleData.button = PointerEventData.InputButton.Middle;

        m_MouseState.SetButtonState(PointerEventData.InputButton.Left, StateForMouseButton(0), leftData);
        m_MouseState.SetButtonState(PointerEventData.InputButton.Right, StateForMouseButton(1), rightData);
        m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, StateForMouseButton(2), middleData);

        return m_MouseState;
    }

    private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
    {
        if (!useDragThreshold)
            return true;

        return (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
    }

    protected virtual void _ProcessMove(PointerEventData pointerEvent)
    {
        var targetGO = pointerEvent.pointerCurrentRaycast.gameObject;
        HandlePointerExitAndEnter(pointerEvent, targetGO);
    }

    protected virtual void _ProcessDrag(PointerEventData pointerEvent)
    {
        bool moving = pointerEvent.IsPointerMoving();

        if (moving && pointerEvent.pointerDrag != null &&
            !pointerEvent.dragging &&
            ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
        {
            ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
            pointerEvent.dragging = true;
        }

        // Drag notification
        if (pointerEvent.dragging && moving && pointerEvent.pointerDrag != null)
        {
            // Before doing drag we should cancel any pointer down state
            // And clear selection!
            if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
            {
                ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);

                pointerEvent.eligibleForClick = false;
                pointerEvent.pointerPress = null;
                pointerEvent.rawPointerPress = null;
            }
            ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
        }
    }

    /// <summary>
    /// Process all mouse events.
    /// </summary>
    private void ProcessMouseEvent()
    {
        var mouseData = GetMousePointerEventData();

        var pressed = mouseData.AnyPressesThisFrame();
        var released = mouseData.AnyReleasesThisFrame();

        var leftButtonData = mouseData.GetButtonState(PointerEventData.InputButton.Left).eventData;

        if (!UseMouse(pressed, released, leftButtonData.buttonData))
            return;

        // Process the first mouse button fully
        ProcessMousePress(leftButtonData);
        //print(leftButtonData.buttonData.pointerCurrentRaycast.gameObject);
        _ProcessMove(leftButtonData.buttonData);
        _ProcessDrag(leftButtonData.buttonData);

        // Now process right / middle clicks
        ProcessMousePress(mouseData.GetButtonState(PointerEventData.InputButton.Right).eventData);
        _ProcessDrag(mouseData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
        ProcessMousePress(mouseData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
        _ProcessDrag(mouseData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);

        if (!Mathf.Approximately(leftButtonData.buttonData.scrollDelta.sqrMagnitude, 0.0f))
        {
            var scrollHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(leftButtonData.buttonData.pointerCurrentRaycast.gameObject);
            ExecuteEvents.ExecuteHierarchy(scrollHandler, leftButtonData.buttonData, ExecuteEvents.scrollHandler);
        }

    }

    private static bool UseMouse(bool pressed, bool released, PointerEventData pointerData)
    {
        if (pressed || released || pointerData.IsPointerMoving() || pointerData.IsScrolling())
            return true;

        return false;
    }

    private bool SendUpdateEventToSelectedObject()
    {
        if (eventSystem.currentSelectedGameObject == null)
            return false;

        var data = GetBaseEventData();
        ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
        return data.used;
    }

    /// <summary>
    /// Process the current mouse press.
    /// </summary>
    private void ProcessMousePress(MouseButtonEventData data)
    {
        var pointerEvent = data.buttonData;
        var currentOverGo = pointerEvent.pointerCurrentRaycast.gameObject;
        
        objectBelowPointer = currentOverGo; //Save object name which is selected right now to be used by other functions

        // PointerDown notification
        if (data.PressedThisFrame())
        {
            pointerEvent.eligibleForClick = true;
            pointerEvent.delta = Vector2.zero;
            pointerEvent.dragging = false;
            pointerEvent.useDragThreshold = true;
            pointerEvent.pressPosition = pointerEvent.position;
            pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;

            DeselectIfSelectionChanged(currentOverGo, pointerEvent);

            // search for the control that will receive the press
            // if we can't find a press handler set the press
            // handler to be what would receive a click.
            var newPressed = ExecuteEvents.ExecuteHierarchy(currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler);

            // didnt find a press handler... search for a click handler
            if (newPressed == null)
                newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

            // Debug.Log("Pressed: " + newPressed);

            float time = Time.unscaledTime;

            if (newPressed == pointerEvent.lastPress)
            {
                var diffTime = time - pointerEvent.clickTime;
                if (diffTime < 0.3f)
                    ++pointerEvent.clickCount;
                else
                    pointerEvent.clickCount = 1;

                pointerEvent.clickTime = time;
            }
            else
            {
                pointerEvent.clickCount = 1;
            }

            pointerEvent.pointerPress = newPressed;
            pointerEvent.rawPointerPress = currentOverGo;

            pointerEvent.clickTime = time;

            // Save the drag handler as well
            pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);

            if (pointerEvent.pointerDrag != null)
                ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
        }

        // PointerUp notification
        if (data.ReleasedThisFrame())
        {
            // Debug.Log("Executing pressup on: " + pointer.pointerPress);
            ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);

            // Debug.Log("KeyCode: " + pointer.eventData.keyCode);

            // see if we mouse up on the same element that we clicked on...
            var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

            // PointerClick and Drop events
            if (pointerEvent.pointerPress == pointerUpHandler && pointerEvent.eligibleForClick)
            {
                ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
            }
            else if (pointerEvent.pointerDrag != null)
            {
                ExecuteEvents.ExecuteHierarchy(currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
            }

            pointerEvent.eligibleForClick = false;
            pointerEvent.pointerPress = null;
            pointerEvent.rawPointerPress = null;

            if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
                ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);

            pointerEvent.dragging = false;
            pointerEvent.pointerDrag = null;

            // redo pointer enter / exit to refresh state
            // so that if we moused over somethign that ignored it before
            // due to having pressed on something else
            // it now gets it.
            if (currentOverGo != pointerEvent.pointerEnter)
            {
                HandlePointerExitAndEnter(pointerEvent, null);
                HandlePointerExitAndEnter(pointerEvent, currentOverGo);
            }
        }
    }
}
