using UnityEngine;

public class RuneCollectible : MonoBehaviour
{
    [Header("Réglages")]
    public int     scoreValue   = 10;
    public float   rotateSpeed  = 90f;    // degrés/seconde — animation visuelle
    public float   bobAmplitude = 0.15f;  // hauteur de l'oscillation
    public float   bobSpeed     = 2f;

    private Vector3 startPos;

    void Start() => startPos = transform.position;

    void Update()
    {
        // Rotation sur l'axe Y
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        // Oscillation verticale (sin)
        Vector3 pos = startPos;
        pos.y += Mathf.Sin(Time.time * bobSpeed) * bobAmplitude;
        transform.position = pos;
    }

    // Déclenché quand un Collider entre dans notre Trigger
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;   // ignorer tout sauf le joueur

        GameManager.Instance.AddScore(scoreValue); // notifier le GameManager
        Destroy(gameObject);                         // supprimer le cristal
    }
}