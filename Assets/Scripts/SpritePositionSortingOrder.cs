using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    [SerializeField] private float positionOffset;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffset) * precisionMultiplier);

        // This sets the sprite's sorting order based on its vertical (Y-axis) position in the world.
        //
        // Formula:
        //   sortingOrder = -(Y + offset) * precision
        //
        // Explanation:
        // 1. `transform.position.y`: gets the object's world Y position.
        // 2. `positionOffset`: lets you tweak the position slightly (useful if sprites are offset from the pivot).
        // 3. `+ positionOffset`: shifts the position to fine-tune layering.
        // 4. `-(...)`: inverts it so that:
        //      - Objects lower on the screen (smaller Y) have higher sorting orders.
        //      - This means they will be drawn *on top* of higher objects, giving a top-down layering effect.
        // 5. `* precisionMultiplier`: scales the value to avoid sortingOrder conflicts between close Y values.
        //      - Higher precisionMultiplier = more separation between layers.
        //
        // Example:
        //   If Y = 2, offset = 0, precision = 5 → sortingOrder = -2 * 5 = -10
        //   If Y = -1, offset = 0, precision = 5 → sortingOrder = -(-1) * 5 = +5 → renders on top


        if (runOnce)
        {
            Destroy(this);
        }
    }


}
