using UnityEngine;

//this class exists to manage the player stats, it will contain all of the information of the stats and exist
//on a player object. It will also contain all of the bars for the visible stats and update them in realtime
//when a stat changes if something is equipped or consumed 
public class PlayerStatsMangager : MonoBehaviour
{
    
    //the stats 
    private float _statAttack = 5;
    private float _statWeight = 10;
    private float _statHealth = 50; 
    private float _maxOut = 100f; 

    //the bars representing the stats 
    [SerializeField] private RectTransform _healthBar; 
    [SerializeField] private RectTransform _weightBar;
    [SerializeField] private RectTransform _attackBar;
    [SerializeField] private float _barWidth, _barHeight; 

    //----------------------
    //Unity Lifetime Methods
    //----------------------
    public void Start()
    {
        changeAttack(0);
        changeWeight(0);
        changeHealth(0); 
    }
    //------------------------------
    //public methods to change stats
    //-------------------------------

    public void changeAttack(int delta)
    {
        _statAttack += delta; 
        
        //clamp it to make health if it goes over
        if (_statAttack > _maxOut)
        {
            _statAttack = _maxOut; 
        }

        //get new width of UI
        //depends on ration from health to max health 
        float newWidth = (_statAttack/_maxOut) * _barWidth; 

        //change the recttransfrom so the width change is actually visible
        _attackBar.sizeDelta = new Vector2(newWidth, _barHeight); 

    }

    public void changeWeight(int delta)
    {
        _statWeight += delta;  

        //clamp it to make health if it goes over
        if (_statWeight > _maxOut)
        {
            _statWeight = _maxOut; 
        }

        //get new width of UI
        //depends on ration from health to max health 
        float newWidth = (_statWeight/_maxOut) * _barWidth; 

        //change the recttransfrom so the width change is actually visible
        _weightBar.sizeDelta = new Vector2(newWidth, _barHeight); 
 
    }

    public void changeHealth(int delta)
    {
        _statHealth += delta; 
        
        //clamp it to max health if it goes over
        if (_statHealth > _maxOut)
        {
            _statHealth = _maxOut; 
        }
        
        //get new width of UI
        //depends on ration from health to max health 
        float newWidth = (_statHealth/_maxOut) * _barWidth; 

        //change the recttransfrom so the width change is actually visible
        _healthBar.sizeDelta = new Vector2(newWidth, _barHeight); 

    }
}