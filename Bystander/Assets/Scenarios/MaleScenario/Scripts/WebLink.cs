using UnityEngine;
using System.Collections;

public class WebLink : MonoBehaviour
{
	public AudioClip ClickSound;
    public WebMenuTransition MyWebMenu;
		public Node webPage;
		private WebSiteManager siteManager;
        private CursorHandler _cursorHandler;
	private AudioManager _audioManager;

		void Start ()
		{
			_audioManager = FindObjectOfType<AudioManager>();
            _cursorHandler = FindObjectOfType<CursorHandler>();
				siteManager = FindObjectOfType<WebSiteManager> ();
				siteManager.AddWebLink (webPage);

		}
	
		void OnMouseDown ()
		{
		_audioManager.PlaySFX (ClickSound, 0.7f, false);
				siteManager.NavigationHandler (webPage);
            if(MyWebMenu != null)
                MyWebMenu.TransitionToMenu();

		}


        void OnMouseEnter()
        {
            _cursorHandler.ChangeCursor(0);
        }

        void OnMouseExit()
        {
            _cursorHandler.ChangeCursor(1);
        }
}
