using UnityEngine;
using System.Collections;

namespace PlantGame.Player 
{
    public class PlayerInteractShopScript : MonoBehaviour
    {
        private EconomyManager EconomyUI;
        [Header("Sign Checking")]
        [SerializeField] private Transform signCheckPivot;
        [SerializeField] private float signCheckSize = 1.5f;
        [SerializeField] private LayerMask signLayer;

        ///// PRIVATE VARIRABLES  /////
        private Rigidbody2D _playerRigidbody;
        private bool _isSignNearby = false;
        private GameObject sign;

        ///// METHODS /////


        void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Greenhouse"){
                EconomyUI = EconomyManager.instance;
                EconomyUI.closeShop();
            }
        }

        void Update(){
            CheckIfSignNearby();
            HandleSignInteract();
        }

        private void HandleSignInteract() 
        {
            if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightControl)))
            {
                if(_isSignNearby)
                {
                    if(EconomyUI == null){
                        EconomyUI = FindFirstObjectByType<EconomyManager>();
                    }
                    EconomyUI.openShop();
                }
                
            }
        }

        private void CheckIfSignNearby()
        {

            Collider2D[] _nearbySigns = Physics2D.OverlapBoxAll(
                signCheckPivot.position, Vector3.one * signCheckSize, 0, signLayer
                );
            CharacterUI characterUI = FindFirstObjectByType<CharacterUI>();
            if(_nearbySigns.Length > 0)
            {
                characterUI?.DisplayShopMessage();
                _isSignNearby = true;
                sign = _nearbySigns[0].gameObject;
            }
            else
            {
                characterUI?.HideTextBox();
                _isSignNearby = false;
            }
        }
    }
}