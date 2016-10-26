using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadingScript : MonoBehaviour {
    
    string sceneName = null;
    bool _doFade = false;

    public Canvas fadePrefab;
    Canvas fadeSprite;
    Image fadeImage;

    float _alpha = 1.0f;
    public float speed;

    public enum FadeType
    {
        FadeIn,
        FadeOut
    } FadeType type;

    void Start () {
        startFade(null, true);
	}

    public void goToScreen(string aScreenName)
    {
        startFade(aScreenName, false);
    }

    public void startFade(string aSceneName, bool aMode = false)
    {
        if(_doFade)
        {
            return;
        }

        this.sceneName = aSceneName;
        _doFade = true;

        if(fadeSprite == null)
        {
            fadeSprite = Instantiate(fadePrefab, fadePrefab.transform.position, Quaternion.identity) as Canvas;
            fadeImage = fadeSprite.GetComponentInChildren<Image>();

        }

        setupFadeTypeAndStartColorWithMode(aMode);
    }

    void setupFadeTypeAndStartColorWithMode(bool aMode)
    {
        Color defaultColor = fadeImage.color;

        if (aMode)
        {
            this.type = FadeType.FadeIn;
            fadeImage.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1.0f);
        }
        else
        {
            this.type = FadeType.FadeOut;
            fadeImage.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.0f);
        }
    }


    void Update ()
    {
        if(_doFade)
        {
            updateAlphaValue();
            Color defaultColor = fadeImage.color;
            fadeImage.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, _alpha);
        }
	}

    void updateAlphaValue()
    {
        switch (type)
        {
            case FadeType.FadeIn:
                doFade(true);
                break;
            case FadeType.FadeOut:
                doFade(false);
                break;

        }
    }

    void doFade(bool aMode)
    {
        if(aMode)
        {
            fadeOutBlackTexture();
        }
        else
        {
            fadeInBlackTexture();
        }

    }

    void fadeOutBlackTexture()
    {
        if (fadeImage.color.a > 0.0f)
        {
            _alpha -= Time.deltaTime * speed;
        }
        else
        {
            _doFade = false;
        }
    }

    void fadeInBlackTexture()
    {
        if (fadeImage.color.a < 1.0f)
        {
            _alpha += Time.deltaTime * speed;
        }
        else
        {
            _doFade = false;
            SceneManager.LoadScene(sceneName);
        }
    }

}
