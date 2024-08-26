using System;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace Game.Services
{
    public class HintManager : MonoBehaviour
    {
        [Inject] private Board _board;
        [Inject] private ParticleService _particleService;
        private MatchFinder _matchFinder;

        private void Start()
        {
            Assert.IsNotNull(_board);
            _matchFinder = new MatchFinder(_board.Rows, _board.Cols);
        }

        private void Update()
        {
            CheckForHint();
        }

        private void CheckForHint()
        {
            for (int x = 0; x < _board.Rows; x++)
            {
                for (int y = 0; y < _board.Cols; y++)
                {
                    if (!_board.Cells[x, y].HasItem()) continue;
                    
                    var item = _board.Cells[x, y].Item;
                    var matchingCells = _matchFinder.FindMatches(_board.Cells[x, y], item.GetMatchType());

                    SetHintSprites(matchingCells.Count, x, y);
                    SetHintParticle(matchingCells.Count, item);
                }
            }
        }

        private void SetHintSprites(int matchCount, int x, int y)
        {
            switch (matchCount)
            {
                case >= MatchHelpers.MinSpecialMatchCount:
                    _board.Cells[x, y].Item.SetHint(matchCount);
                    break;
                
                case < MatchHelpers.MinSpecialMatchCount:
                    _board.Cells[x, y].Item.SetDefaultItemSprite();
                    break;
            }
        }

        private void SetHintParticle(int matchCount, Item item)
        {
            if (matchCount >= 2 && item.GetMatchType() == MatchType.SpecialType)
            {
                if (!item.IsParticlePlaying())
                {
                    Debug.LogError("Play");
                    _particleService.Spawn("ComboParticle", item.transform.position);
                }
            }
            else
            {
                if (item.IsParticlePlaying())
                {
                    Debug.LogError("Stop");
                    _particleService.Despawn("ComboParticle", item.HintParticle);
                }
            }
        }
    }
}