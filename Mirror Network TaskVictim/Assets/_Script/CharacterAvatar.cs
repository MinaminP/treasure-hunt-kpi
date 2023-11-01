using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterAvatar : NetworkBehaviour
{
    [SyncVar(hook = nameof(SetAvatar))]
    public int chosenAvatarId;

    Animator anim;

    public GameObject[] avatars;
    //public Avatar[] animationAvatar;
    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].SetActive(false);
        }
    }
    public override void OnStartAuthority()
    {
        chosenAvatarId = LocalPlayerData.avatarId;

        avatars[chosenAvatarId].SetActive(true);
        for (int i = 0; i < avatars.Length; i++)
        {
            if (i == chosenAvatarId)
            {
                avatars[i].gameObject.SetActive(true);
            }
            else
            {
                avatars[i].SetActive(false);
            }
        }
        //anim.avatar = animationAvatar[chosenAvatarId];
        //anim.avatar = avatars[chosenAvatarId].gameObject.GetComponent<Animator>().avatar;
        //anim.runtimeAnimatorController = avatars[chosenAvatarId].GetComponent<Animator>().runtimeAnimatorController;

        CmdSetAvatar(chosenAvatarId);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetAvatar(int _avaId)
    {
        chosenAvatarId = _avaId;
        Debug.Log(chosenAvatarId);
    }

    public void SetAvatar(int oldAvatar, int newAvatar)
    {
        newAvatar = chosenAvatarId;
        Debug.Log(newAvatar);

        avatars[newAvatar].SetActive(true);
        for (int i = 0; i < avatars.Length; i++)
        {
            if (i == newAvatar)
            {
                avatars[i].gameObject.SetActive(true);
            }
            else
            {
                avatars[i].SetActive(false);
            }
        }
        //anim.avatar = animationAvatar[chosenAvatarId];
        //anim.avatar = avatars[newAvatar].GetComponent<Animator>().avatar;
        //anim.runtimeAnimatorController = avatars[newAvatar].GetComponent<Animator>().runtimeAnimatorController;
    }
}
