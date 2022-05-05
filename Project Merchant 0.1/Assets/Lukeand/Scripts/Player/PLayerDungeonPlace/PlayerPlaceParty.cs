using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceParty : MonoBehaviour
{
    List<CharacterAlly> partyList = new List<CharacterAlly>();

    public void SetUpPartyList(List<CharacterAlly> _partyList)
    {
        partyList = _partyList;
    }
}
