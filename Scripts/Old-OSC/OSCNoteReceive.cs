using UnityEngine;
using extOSC;

public class OSCNoteReveive : MonoBehaviour
{
	public OSCReceiver receiver;
	public Transform sphere;

	private float noteValue = 0f;
	private float noteMod = 0f;

	void Start()
	{
		receiver.Bind("/note", OnTestMessage);
	}

	void OnTestMessage(OSCMessage message)
	{ 
		if (message.Values.Count > 0)
		{
			noteValue = message.Values[0].IntValue;

			Debug.Log("Received note: " + noteValue);
			
			noteMod = (noteValue % 12) - 5.5f;
		}
	}

	void Update()
	{
		sphere.position = new Vector3(sphere.position.x, sphere.position.y, noteMod);
	}
}