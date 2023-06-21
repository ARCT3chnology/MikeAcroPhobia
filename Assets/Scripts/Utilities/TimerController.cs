using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class TimerController : MonoBehaviourPunCallbacks
{
    private int timerDuration = 30000; // Duration of the timer in milliseconds
    private int timerStartTime; // Start time of the timer
    private bool timerRunning = false; // Flag to indicate if the timer is currently running

    private void Start()
    {
        // Subscribe to the OnEventReceived event
        PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    }


    private void OnDestroy()
    {
        // Unsubscribe from the OnEventReceived event
        PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
    }



    private void Update()
    {
        if (timerRunning)
        {
            int currentTime = (int)(PhotonNetwork.ServerTimestamp - timerStartTime);

            if (currentTime >= timerDuration)
            {
                // Timer has reached the desired duration, handle completion
                HandleTimerCompletion();
            }
        }
    }



    // Method to start the timer
    public void StartTimer()
    {
        // Only the master client can start the timer
        if (PhotonNetwork.IsMasterClient)
        {
            timerStartTime = (int)PhotonNetwork.ServerTimestamp;
            timerRunning = true;

            // Broadcast the timer start time to all clients
            object[] eventData = new object[] { timerStartTime };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(1, eventData, raiseEventOptions, SendOptions.SendReliable);
        }
    }
    


    // Method to handle the completion of the timer
    private void HandleTimerCompletion()
    {
        // Reset the timer
        timerRunning = false;

        // Perform any actions you need when the timer is completed
        Debug.Log("Timer completed!");
    }



    // Event handler for receiving custom events
    private void OnEventReceived(EventData photonEvent)
    {
        // Check if the event is the timer start event
        if (photonEvent.Code == 1)
        {
            // Extract the timer start time from the event data
            object[] eventData = (object[])photonEvent.CustomData;
            timerStartTime = (int)eventData[0];
            timerRunning = true;
        }
    }
}
