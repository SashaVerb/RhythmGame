using UnityEngine;

public partial class TextEffect
{
    public class Factory
    {
        private Transform _container;
        private TextEffect _prefab;
        private Canvas _canvas;
        private Camera _camera;

        public Factory(Transform container, TextEffect prefab, Canvas canvas, Camera camera)
        {
            _container = container;
            _prefab = prefab;
            _canvas = canvas;
            _camera = camera;
        }

        public TextEffect Create(Vector3 position, string text)
        {
            var effect = Object.Instantiate(_prefab, _container);

            Vector2 screenPoint = _camera.WorldToScreenPoint(position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                screenPoint,
                null,
                out var localPoint);

            effect.transform.localPosition = localPoint;

            effect.Show(text);

            return effect;
        }
    }
}
