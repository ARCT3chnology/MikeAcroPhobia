using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    //[SerializeField] List<string> data = new List<string> { "fname", "lname", "home", "home", "company" };
    private void Start()
    {
        testMethod();
        //allPlayersGotSameVote();
        //OneplayerGotMaxVotes();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Get the mouse position
                Vector3 mousePosition = Input.mousePosition;

                // Create a pointer event
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = mousePosition;

                // Perform the raycast and store the results
                List<RaycastResult> results = new List<RaycastResult>(); // Adjust the size based on your needs
                EventSystem.current.RaycastAll(eventData, results);

                // Iterate through the raycast results
                for (int i = 0; i < results.Count; i++)
                {
                    GameObject hitObject = results[i].gameObject;

                    // Check if the hit object is a UI element
                    if (hitObject.GetComponent<UIBehaviour>() != null)
                    {
                        // A UI element was hit by the raycast
                        // Implement your logic based on the hit information
                        Debug.Log("Hit UI element: " + hitObject.name);
                        break; // Exit the loop after the first UI hit
                    }
                }
            }
        }

        //Debug.Log("11");
    }

    private static void testMethod()
    {
        int[] names = new int[] { 1, 1, 0, 0 };

        var duplicatesWithIndices = names
            // Associate each name/value with an index
            .Select((Name, Index) => new { Name, Index })
            // Group according to name
            .GroupBy(x => x.Name)
            //descending order
            // Only care about Name -> {Index1, Index2, ..}
            .Select(xg => new
            {
                Name = xg.Key,
                Indices = xg.Select(x => x.Index)
            })
            .OrderByDescending(x => x.Name)
            // And groups with more than one index represent a duplicate key
            .Where(x => x.Indices.Count() > 1);

        // Now, duplicatesWithIndices is typed like:
        //   IEnumerable<{Name:string,Indices:IEnumerable<int>}>
        // Let's say we print out the duplicates (the ToArray is for .NET 3.5):
        foreach (var g in duplicatesWithIndices)
        {
            //Debug.Log("Players: " + g.Indices.ToArray().Count());

            for (int i = 0; i < g.Indices.ToArray().Count(); i++)
            {
                if (g.Name == names.Max())
                {
                    //faceOffPlayers.Add(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                    Debug.Log("Players: " + g.Indices.ToArray()[i]);
                    //startFaceOffRound(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                }
            }
        }

        // The output for the above input is:
        // > Have duplicate a with indices 0,1,4
        // > Have duplicate b with indices 3,5
    }

    public static bool allPlayersGotSameVote()
    {
        bool state;
        int[] allVotes = new int[4] {1,1,2,1};
        //for (int i = 0; i < allVotes.Length; i++)
        //{
        //    allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        //}
        state = allVotes.ToList().Distinct().Count() == 1 ? true : false;
        Debug.Log("all votes are: " + state);
        return state;
    }

    public static bool OneplayerGotMaxVotes()
    {
        bool state;
        //int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        int[] allVotes = new int[4] { 1, 1, 2, 1 };

        //for (int i = 0; i < allVotes.Length; i++)
        //{
        //    allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        //}
        int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
        state = maxCount == 1 ? true : false;
        Debug.Log("One player got max votes: " + state);
        return state;
    }

    public static bool playerGotSameMaxVotes()
    {
        bool state;
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
        state = maxCount > 1 ? true : false;
        return state;
    }


}
