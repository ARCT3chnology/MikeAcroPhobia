using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    //[SerializeField] List<string> data = new List<string> { "fname", "lname", "home", "home", "company" };
    private void Start()
    {
        testMethod();
        //allPlayersGotSameVote();
        //OneplayerGotMaxVotes();
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
