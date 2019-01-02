//using Core;
//using Framework.Types;
//using Modules.Abilityable;
//using Modules.Magicable;
//using Modules.Healthable;
//using Modules.Teamable;
//using Triggers;
//using UnityEngine;
//using Util;
//
//namespace Entity.Abilities.WarpedMagi
//{
//    [CreateAssetMenu(fileName = "WarpedMagi_Infusion.asset", menuName = "Ability/WarpedMagi/Infusion")]
//    public class Infusion : Ability
//    {
//        [SerializeField] private float _manaCost = 100f;
//        [SerializeField] private float _manaSteal = 50f;
//        [SerializeField] private float _manaStealSearchRadius = 5f;
//        [SerializeField] private float _castRange = 5f;	
//
//		
//		private ITeamable _teamable;
//		private IMagicable _magicable;
//
//	    protected override void Initialize()
//
//        {
//			_teamable = Self.GetComponent<ITeamable>();
//			_magicable = Self.GetComponent<IMagicable>();
//        }
//
//	    public override void Terminate()
//	    {
//		    //Nothing to Terminate
//	    }
//
//	    protected override void Cast()
//        {
//            RaycastHit hit;
//            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) return;
//
//            if ((hit.point - Self.transform.position).sqrMagnitude > _castRange * _castRange)
//                return;
//
//
//            if (_magicable.ManaPoints < _manaCost)
//                return;
//
//            var col = hit.collider;
//            var go = col.gameObject;
//            if (col.attachedRigidbody != null)
//                go = col.attachedRigidbody.gameObject;
//
//            _magicable.ModifyMana(-_manaCost, Self);
//            ApplyInfusion(go);
//        }
//		public void ApplyInfusion(GameObject target)
//		{
//			var cols = Physics.OverlapSphere(target.transform.position, _manaStealSearchRadius, (int)LayerMaskHelper.Entity); 
//			var gos = Triggers.Trigger.GetGameObjectFromColliders(cols);
//			
//			var teamable = target.GetComponent<ITeamable>();
//			var magicable = target.GetComponent<IMagicable>();
//			var healthable = target.GetComponent<IHealthable>(); 			
//
//			var totalManaStolen = 0f;
//			foreach(var go in gos)
//			{
//				if(go == target)
//					continue;
//				
//				if(_teamable == null || teamable == null || teamable.Team == _teamable.Team)
//					continue;
//				
//				var enemyMagicable = go.GetComponent<IMagicable>();
//
//				if(enemyMagicable == null)
//					continue;
//				
//				var enemyManaPoints = enemyMagicable.ManaPoints;
//				var stolen = Mathf.Min(enemyManaPoints, _manaSteal);
//				enemyMagicable.ModifyMana(-stolen, Self);
//		
//				totalManaStolen += stolen;
//			}
//			
//			magicable.ModifyMana(totalManaStolen, Self);
//			// //If allied, dont deal damage
//			// if(_teamable != null && teamable != null && _teamable.Team == teamable.Team)
//				// return;
//			
//			var damage = new Damage(totalManaStolen,DamageType.Magical,Self);
//			healthable.TakeDamage(damage);
//		}
//    }
//}