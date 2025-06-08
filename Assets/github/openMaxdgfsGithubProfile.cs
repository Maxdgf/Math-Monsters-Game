using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openMaxdgfsGithubProfile : MonoBehaviour
{
    private const string MaxdgfsUrlProfile = "https://github.com/Maxdgf";

    public void openProfile()
    {
        Application.OpenURL(MaxdgfsUrlProfile);
    }
}
