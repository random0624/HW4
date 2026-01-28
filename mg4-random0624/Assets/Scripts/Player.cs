
using UnityEngine;

public class Player : MonoBehaviour{
[SerializeField] private Rigidbody2D _rigidbody;
[SerializeField] private Collider2D _collider;
[SerializeField] private float _jump;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flap();
    }

    void Flap()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            _rigidbody.velocity = new Vector2(
                _rigidbody.velocity.x,
                _jump
            );
        }
    }
}
