using UnityEngine;
using UnityEngine.InputSystem;
using extOSC;

public class OSCFlagTransmit : MonoBehaviour
{
	public OSCTransmitter transmitter;

	void Update()
	{
		if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
		{
			OSCMessage message = new OSCMessage("/test");
			message.AddValue(OSCValue.Int(123));

			transmitter.Send(message);

			Debug.Log("Sent: /test 123");
		}
	}
}