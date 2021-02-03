using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class OrderedRaycaster : GraphicRaycaster
    {
 
        [SerializeField] private int m_sortOrder = 0;

        public override int sortOrderPriority => m_sortOrder;
    }
}
