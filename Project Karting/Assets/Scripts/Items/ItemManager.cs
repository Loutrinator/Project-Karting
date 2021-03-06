﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Items
{
    [Serializable]
    public class ItemProbability
    {
        public float probability;
        public int itemId;
    }
    
    #region ItemManager
    [CreateAssetMenu(fileName="ItemManagerPreset",menuName="ScriptableObject/ItemManager",order=0)]
    public class ItemManager : ScriptableObject
    {
        [HideInInspector, SerializeField] public int nbItems;
        [HideInInspector, SerializeField] public int nbPositions;
        [HideInInspector, SerializeField] public List<Item> items;
        [HideInInspector, SerializeField] public List<ListProbability> itemProbabilities;
        
       

/*
        private void Start()
        {
            //GenerateProbabilities();
        }

        private void GenerateProbabilities()
        {
            itemProba = new List<List<ItemProbability>>();
            nbPositions = GameManager.Instance.nbPlayerRacing;
            //nbItems = items.Count;
            for (int i = 0; i < nbPositions; i++)
            {
                float sumProba = 0f;
                itemProba.Append(new List<ItemProbability>());
                for (int j = 0; j < nbItems; j++)
                {
                    float proba = probabilities[i][j]; //[i][j]
                    if (proba > 0)
                    {
                        sumProba += probabilities[i][j]; //[i][j]

                        if (sumProba > 1f)
                        {
                            //Debug.LogError("SUM OF PROBABILITIES AT POSITION " + i + " IS OVER 1f");
                        }
                        else
                        {
                            ItemProbability currentItemProba = new ItemProbability();
                            currentItemProba.probability = sumProba;

                            itemProba[i].Append(currentItemProba);
                        }
                    }
                }

                //TODO: TRIER LES LISTES D'ITEMPROBABILITY PAR PROBABILITE
            }
        }
*/

        [CanBeNull]
        public Item GetRandomItem(int position)
        {
            float rnd = Random.value;
            for (int i = 0; i < itemProbabilities[position].Count; i++)
            {
                ItemProbability proba = itemProbabilities[position][i];
                if (rnd <= proba.probability)
                {
                    return items[proba.itemId];
                }
            }
            return null;
        }
    }
    #endregion
    #region Editor
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(ItemManager))]
    public class ItemManagerEditor : Editor
    {
        private int currentPosition = 0;
        List<string> positionString = new List<string>();
        private static Color[] itemColors;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var itemManager = (ItemManager) target;
            if (itemManager == null) return;
            if (itemManager.itemProbabilities == null) itemManager.itemProbabilities = new List<ListProbability>();
            if (itemManager.items == null) itemManager.items = new List<Item>();
            if (itemColors == null) itemColors = new  Color[itemManager.items.Count];
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Positions");
            EditorGUILayout.Space();
            itemManager.nbPositions =  Math.Max(1,EditorGUILayout.IntField("Position count",itemManager.nbPositions));
            while (itemManager.nbPositions > positionString.Count)
            {
                positionString.Add("Position "+ (positionString.Count+1));
                itemManager.itemProbabilities.Add(new ListProbability());
            }

            while (itemManager.nbPositions < positionString.Count)
            {
                positionString.RemoveAt(positionString.Count - 1);
                itemManager.itemProbabilities.RemoveAt(positionString.Count - 1);
            }
            
            DrawItems(itemManager);

            DrawProbability(itemManager);
        }
        
        private static void DrawItems(ItemManager itemManager)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Items");
            EditorGUILayout.Space();

            List<Item> list = itemManager.items;
            itemManager.nbItems = Math.Max(0, EditorGUILayout.IntField("Size", list.Count));

            while (itemManager.nbItems > list.Count)
            {
                list.Add(null);
                updateColors(list.Count);
            }

            while (itemManager.nbItems < list.Count)
            {
                list.RemoveAt(list.Count - 1);
                updateColors(list.Count);
            }
            EditorGUI.indentLevel++;
            for (int i = 0; i < list.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                list[i] = EditorGUILayout.ObjectField("Item " + i, list[i], typeof(Item),
                    true) as Item;
                //EditorGUI.DrawRect(GUILayoutUtility.GetRect(5,10,20,20), itemColors[i]); affichage de couleur
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;
        }

        private static void updateColors(int amount)
        {
            itemColors = new Color[amount];
            for (int i = 0; i < amount; i++)
            {
                float hue = (i + 1f) / (float) amount;
                itemColors[i] = Color.HSVToRGB(hue, 0.7f, 1f);
            }
        }

        private void DrawProbability(ItemManager itemManager)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Probabilities");
            EditorGUILayout.Space();
            GUIContent arrayList = new GUIContent("Position");
            currentPosition = EditorGUILayout.Popup(arrayList, currentPosition, positionString.ToArray());
            
            List<string> itemNames = new List<string>();
            foreach (var item in itemManager.items)
            {
                if(item != null) itemNames.Add(item.GetName());
            }
            
            
            //List<ItemProbability> probas = itemManager.itemProba[currentPosition];
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", GUILayout.MaxWidth(20)))
            {
                itemManager.itemProbabilities[currentPosition].Add(new ItemProbability());
            }
            EditorGUILayout.EndHorizontal();
            float sumProba = 0f;
            string promptProba = "";
            for (int i = 0; i < itemManager.itemProbabilities[currentPosition].Count; i++)
            {
                bool removed = false;
                ItemProbability p = itemManager.itemProbabilities[currentPosition][i];EditorGUILayout.BeginHorizontal();
                GUIContent itemList = new GUIContent("Item");
                p.itemId = EditorGUILayout.Popup(itemList, p.itemId, itemNames.ToArray());
                if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                {
                    itemManager.itemProbabilities[currentPosition].RemoveAt(i);
                    removed = true;
                }
                EditorGUILayout.EndHorizontal();
                if (!removed)
                {
                    float value = Math.Min(1-sumProba,EditorGUILayout.Slider("Probability", itemManager.itemProbabilities[currentPosition][i].probability-sumProba, 0f, 1f));
                    sumProba += value; 
                    p.probability = sumProba;
                    promptProba += "| " + i + " : " + p.probability + " ";
                    itemManager.itemProbabilities[currentPosition][i] = p;
                    EditorGUILayout.Separator();
                }
                
            }
            //Debug.Log(promptProba);
        }

    }
    #endif
    
    #endregion
}
