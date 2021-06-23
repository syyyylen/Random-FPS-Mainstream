using UnityEngine;

namespace GD2
{
    public class SynchVar<T> : ScriptableObject
    {
        [SerializeField] private T m_var;
        public delegate void VarChangedDelegate(T p_var);
        public event VarChangedDelegate OnVarChanged;

        public T Value
        {
            get { return m_var; }
            set
            {
                if (m_var.Equals(value)) return;
                m_var = value;
                if (OnVarChanged != null) OnVarChanged(m_var);
            }
        }
    }
}