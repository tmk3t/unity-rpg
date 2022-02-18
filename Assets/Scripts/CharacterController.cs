using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 自分が発射した弾がぶつかってダメージを受けてしまうのを防ぐ。
 */
public class  CharacterController : MonoBehaviour
{
  //   //グループの列挙。
  //   public enum HitGroup
  //   {
  //       //自分自身
  //       Player1,
  //       //相手グループ
  //       Player2,
  //       //その他
  //       Other
  //   }

  //   //自分の当たり判定グループ
  //   public HitGroup hitGroup = HitGroup.Player1;

  //   protected bool IsHitOK (GameObject hittedObject)
  //   {
  //       //相手が同じスクリプト（HitObject）を持っているかどうかを確認する。
  //       HitObject hit = hittedObject.GetComponent<HitObject>();
  //       //持っていなければ当たり判定は不要
  //       if(hit == null)
  //       {
  //           return false;
  //       }

  //       //ヒットしたオブジェクトが自分と同じグループであれば当たり判定は不要。
  //       if (hitGroup == hit.hitGroup)
  //       {
  //           return false;
  //       }
  //       return true;
  //   }

  //   private void private void OnTriggerEnter(Collider other) {
  //       if(IsHitOK(hitCollider gameObject)==false){
  //     return;
  //   }
  //   if(hitEffectPrefab != null){
  //     Instantiate(hitEffectPrefab, transform.position, transform.rotation);
  //   }
  //   }

  // Vector3 move = (Vector3.forward * bulletMoveSpeed);
  // transform.position
}
