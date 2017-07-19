using Playground;
using Playground.Utilities;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class NumberCard
{
    private const float POSITION_Y = 0.35f;
    private const float POSITION_X_FACTOR = -0.17f;
    private static GameObject prefab = Resources.Load<GameObject>("NumberContainer");

    private GameObject card;
    private Coroutine coroutine;

    public int Number { get; private set; }
    public bool IsMoving { get; private set; }
    public Vector2 Position
    {
        get { return card.transform.localPosition; }
        set { card.transform.localPosition = value; }
    }

    public NumberCard(int number, Transform parent)
    {
        card = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
        this.Number = number;
        
        MakeInvisibleIfZero();
        SetTextPosition();
        SetCardText();
    }

    private void MakeInvisibleIfZero()
    {
        if (Number == 0)
        {
            card.transform.localScale = Vector3.zero;
        }
    }

    private void SetTextPosition()
    {
        Vector2 textPosition = GetTextPosition();
        Transform childTransform = card.transform.GetChild(0);
        childTransform.localPosition = textPosition;
    }

    private void SetCardText()
    {
        Transform childTransform = card.transform.GetChild(0);
        TextMesh textMesh = childTransform.GetComponent<TextMesh>();
        
        textMesh.text = Number.ToString();
    }

    private Vector2 GetTextPosition()
    {
        Vector2 textPosition;
        if (Number < 10)
            textPosition = new Vector2(POSITION_X_FACTOR * 1, POSITION_Y);
        else if (Number < 100)
            textPosition = new Vector2(POSITION_X_FACTOR * 2, POSITION_Y);
        else
            textPosition = new Vector2(POSITION_X_FACTOR * 3, POSITION_Y);
        return textPosition;
    }

    public void MoveTo(Vector2 target, float speed)
    {
        if (coroutine != null)
        {
            Singleton<MonoBehaviour>.Instance.StopCoroutine(coroutine);
        }
        coroutine = Singleton<MonoBehaviour>.Instance.StartCoroutine(AsyncMoveTo(target, speed));
    }

    private IEnumerator AsyncMoveTo(Vector2 target, float speed)
    {
        IsMoving = true;

        while (Position != target)
        {
            Position = Vector2.MoveTowards(Position, target, speed);
            yield return new WaitForFixedUpdate();
        }

        IsMoving = false;
        coroutine = null;
    }
}

