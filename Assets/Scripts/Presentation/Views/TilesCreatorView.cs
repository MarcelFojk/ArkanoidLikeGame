using Universal.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;
using Universal;
using Presentation.Data;

namespace Presentation.Views
{
    class TilesCreatorView : MonoBehaviour
    {
        List<TileData> _tilesData = new();
        List<AbstractTileView> _tileViews = new();

        [SerializeField]
        Transform _tilesContainer;

        [SerializeField]
        TilesCreatorData _tilesCreatorData;

        AbstractTileView[] _tilePrefabs;
        float _multipleHitTilePercent;
        float _movingtTilePercent;
        int _heightInTiles;
        int _widthInTiles;
        float _tileHeight;
        float _tileWidth;

        internal void Awake()
        {
            _tilePrefabs = _tilesCreatorData.TilePrefabs;
            _multipleHitTilePercent = _tilesCreatorData.MultipleHitTilePercent;
            _movingtTilePercent = _tilesCreatorData.MovingtTilePercent;
            _heightInTiles = _tilesCreatorData.HeightInTiles;
            _widthInTiles = _tilesCreatorData.WidthInTiles;
            _tileHeight = _tilesCreatorData.TileHeight;
            _tileWidth = _tilesCreatorData.TileWidth;

        }

        internal (List<AbstractTileView>, List<TileData>) CreateLevel()
        {   
            Random.InitState(UniversalData.CurrentLevel);

            Assert.IsFalse(_multipleHitTilePercent + _movingtTilePercent > 1f, "Sum of tile percents is greater than 100%");

            return CreateSquare();
        }

        internal List<AbstractTileView> LoadLevel(List<TileData> tilesData)
        {   
            List<AbstractTileView> tileViews = new();

            for (int i = 0; i < tilesData.Count; i++)
            {
                AbstractTileView view = Instantiate(_tilePrefabs[(int)tilesData[i].Type], _tilesContainer);
                view.transform.localPosition = new Vector3(tilesData[i].PositionX, tilesData[i].PositionY, 0f);

                tileViews.Add(view);
            }

            return tileViews;
        }

        internal void DestroyTile(AbstractTileView tileView) => Destroy(tileView.gameObject);

        private (List<AbstractTileView>, List<TileData>) CreateSquare()
        {
            AbstractTileView[,] downRightQuarter = new AbstractTileView[_heightInTiles / 2, _widthInTiles / 2];

            for (int i = 0; i < downRightQuarter.GetLength(0); i++)
                for (int j = 0; j < downRightQuarter.GetLength(1); j++)
                {
                    AbstractTileView tilePrefab = GetTilePrefab();
                    AbstractTileView view = Instantiate(tilePrefab, _tilesContainer);

                    view.transform.localPosition = new Vector2(GetXPosition(j), GetYPosition(i));
                    downRightQuarter[i, j] = view;

                    _tileViews.Add(view);
                    _tilesData.Add(new TileData(view.transform.localPosition, view.Type));
                }

            ReplicateQuarterToWhole(downRightQuarter);

            return (_tileViews, _tilesData);
        }


        // interpolates down left quarter
        private void ReplicateQuarterToWhole(AbstractTileView[,] tileViews)
        {
            for (int k = 0; k < 3; k++)
            {   
                if (k == 0)
                {
                    for (int i = 0; i < tileViews.GetLength(0); i++)
                        for (int j = 0; j < tileViews.GetLength(1); j++)
                        {
                            AbstractTileView tileView = Instantiate(_tilePrefabs[(int)tileViews[i, j].Type], _tilesContainer);
                            tileView.transform.localPosition = new Vector2(-1 * GetXPosition(j), GetYPosition(i));

                            _tileViews.Add(tileView);
                            _tilesData.Add(new TileData(tileView.transform.localPosition, tileView.Type));
                        }
                }
                else if (k == 1)
                {
                    for (int i = 0; i < tileViews.GetLength(0); i++)
                        for (int j = 0; j < tileViews.GetLength(1); j++)
                        {
                            AbstractTileView tileView = Instantiate(_tilePrefabs[(int)tileViews[i, j].Type], _tilesContainer);
                            tileView.transform.localPosition = new Vector2(GetXPosition(j), -1 * GetYPosition(i));

                            _tileViews.Add(tileView);
                            _tilesData.Add(new TileData(tileView.transform.localPosition, tileView.Type));
                        }
                }
                else
                {
                    for (int i = 0; i < tileViews.GetLength(0); i++)
                        for (int j = 0; j < tileViews.GetLength(1); j++)
                        {
                            AbstractTileView tileView = Instantiate(_tilePrefabs[(int)tileViews[i, j].Type], _tilesContainer);
                            tileView.transform.localPosition = new Vector2(-1 * GetXPosition(j), -1 * GetYPosition(i));

                            _tileViews.Add(tileView);
                            _tilesData.Add(new TileData(tileView.transform.localPosition, tileView.Type));
                        }
                }
            }
        }

        private AbstractTileView GetTilePrefab()
        {
            float random = Random.Range(0f, 1f);

            if (random < _multipleHitTilePercent)
                return _tilePrefabs[1];
            if (random < _multipleHitTilePercent + _movingtTilePercent && random > _multipleHitTilePercent)
                return _tilePrefabs[2];
            else
                return _tilePrefabs[0];
        }

        private float GetXPosition(int counter)
        {
            return counter * _tileWidth + _tileWidth / 2f;
        }

        private float GetYPosition(int counter)
        {
            return (-_tileHeight * counter - _tileHeight / 2f);
        }
    }
}