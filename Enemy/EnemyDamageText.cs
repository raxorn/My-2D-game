using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDamageText : MonoBehaviour
{
    public static EnemyDamageText Create(Vector2 position, int damageAmount, bool isCrit){
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        EnemyDamageText dmgPopup = damagePopupTransform.GetComponent<EnemyDamageText>();
        dmgPopup.Setup(damageAmount, isCrit);

        return dmgPopup;
    }

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake(){
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCrit){
        textMesh.SetText(damageAmount.ToString());
        if(isCrit){
            textMesh.fontSize = 10;
            textColor = new Color32(255, 0, 0, 255);
        }
        else{
            textMesh.fontSize = 4;
            textColor = new Color32(255, 255, 255, 255);
        }
        textMesh.color = textColor;
        disappearTimer = 1.5f;

        moveVector = new Vector3(Random.Range(-1,1 ),Random.Range(-1,1 )) * Random.Range(4,8 );
    }


    private void Update(){
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * Random.Range(2,6 )* Time.deltaTime;


        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0){
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0){
                Destroy(gameObject);
            }
        }
    }








}
