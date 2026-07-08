using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;

        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
/*
        bool sniper;
        bool normal;
        bool minigun; */

        // Use this for initialization
        private void Start()
        { 
            FollowWeaponOnStart();

            /* sniper = myPlayerSelection.GetSniper();
            normal = myPlayerSelection.GetNormal();
            minigun = myPlayerSelection.GetMinigun(); */
            
            /* if (normal == true)
            {
                FollowWeaponOnStart(target);
            }

            if (sniper == true)
            {
                FollowWeaponOnStart(targetPlayerSniper);
            }

            if (minigun == true)
            {
                FollowWeaponOnStart(targetPlayerMinigun);
            } */
        }

        /* private void FollowNormal()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        private void FollowSniper()
        {
            m_LastTargetPosition = targetPlayerSniper.position;
            m_OffsetZ = (transform.position - targetPlayerSniper.position).z;
            transform.parent = null;
        }

        private void FollowMinigun()
        {
            m_LastTargetPosition = targetPlayerMinigun.position;
            m_OffsetZ = (transform.position - targetPlayerMinigun.position).z;
            transform.parent = null;
        }
 */

        // Update is called once per frame
        private void Update()
        {
            FollowWeaponOnUpdate();
            /* if (normal == true)
            {
                FollowWeaponOnUpdate(target);
            }

            if (sniper == true)
            {
                FollowWeaponOnUpdate(targetPlayerSniper);
            }

            if (minigun == true)
            {
                FollowWeaponOnUpdate(targetPlayerMinigun);
            } */
        }

        /* private void FollowNormal2()
        {
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }

        private void FollowSniper2()
        {
            float xMoveDelta = (targetPlayerSniper.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = targetPlayerSniper.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = targetPlayerSniper.position;
        }

        private void FollowMinigun2()
        {
            float xMoveDelta = (targetPlayerMinigun.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = targetPlayerMinigun.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = targetPlayerMinigun.position;
        } */

        private void FollowWeaponOnStart()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }

        private void FollowWeaponOnUpdate()
        {
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }
    }
}
