using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;

public class CreateLevel : EditorWindow
{
    private ChapterObject _chapter;

    private LevelObject _currentLevel;
    private int _currentNumberLevel = 0;

    private int _countBackground = 0;
    private int _countBlock = -1;
    private int _countEffect = -1;

    private int _countgroundX = 8;
    private int _countGroundY = 8;

    private FieldSquare[,] _levelSquares;

    private string UXML_FILE_PATH = "Assets/Scripts/Editor/CreateLevel.uxml";
    private string USS_FILE_PATH = "Assets/Scripts/Editor/CreateLevel.uss";
    private string LEVELS_FILE_PATH = "Assets/Resources/Chapters/Chapter{0}/Level{1}.asset";

    [MenuItem("Arcanoid Levels/Create Level")]
    public static void ShowExample()
    {
        CreateLevel wnd = GetWindow<CreateLevel>();
        wnd.titleContent = new GUIContent("Create Level");
    }

    private void OnEnable()
    {
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UXML_FILE_PATH);
        VisualElement uxmlRoot = visualTree.CloneTree();
        root.Add(uxmlRoot);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(USS_FILE_PATH);

        var preMadeStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(USS_FILE_PATH);

        root.styleSheets.Add(styleSheet);

        root.styleSheets.Add(preMadeStyleSheet);

        root.Q<VisualElement>("Container").style.height = new StyleLength(position.height);

        //Object Field
        var presetObjectField = root.Q<ObjectField>("LoadChapter");

        presetObjectField.objectType = typeof(ChapterObject);

        presetObjectField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(e =>
        {
            if (presetObjectField.value != null)
            {
                _chapter = (ChapterObject)presetObjectField.value;
            }

            PopulatePresetList();
            SearchLevels();
        });

        //button for increment and decrement
        Button decrementButton = rootVisualElement.Q<Button>("DownLevel");
        Button incrementButton = rootVisualElement.Q<Button>("UpLevel");

        decrementButton.style.width = 25f;
        decrementButton.style.height = 50f;

        decrementButton.clickable.clicked += () =>
        {
            if (_chapter != null && _currentNumberLevel >= 1)
            {                
                _currentNumberLevel--;
                rootVisualElement.Q<TextField>("Level").value = _currentNumberLevel.ToString();
                OpenLevel();
            }            
        };

        incrementButton.clickable.clicked += () =>
        {
            Debug.Log(_chapter + "!!!" + _currentNumberLevel + "!!!" + _chapter.CountLevel);
            if (_chapter != null && _currentNumberLevel < _chapter.CountLevel + 1)
            {   
                _currentNumberLevel++;
                rootVisualElement.Q<TextField>("Level").value = _currentNumberLevel.ToString();
                OpenLevel();
            }
        };

        //Handling button callbacks for working with files
        Button saveButton = rootVisualElement.Q<Button>("SaveButton");
        Button deliteButton = rootVisualElement.Q<Button>("DeleteButton");

        saveButton.clickable.clicked += OnSavePreset;
        deliteButton.clickable.clicked += OnDelitePreset;
    }
    
    private void PopulatePresetList()
    {
        if(_chapter != null)
        {
            FillPopulatePresetList(_chapter._backgrounds, "Background", 50f, 50f, SelectBackground);


            Texture[] textures = null;
            if(_chapter._blocks != null)
            {
                textures = new Texture[_chapter._blocks.Length];
                for (int i = 0; i < _chapter._blocks.Length; i++)
                {                    
                    textures[i] = _chapter._blocks[i]._iconBlock;                    
                }
            }

            FillPopulatePresetList(textures, "Block", 50f, 50f, SelectBlock);

            if (_chapter._effects != null)
            {
                textures = new Texture[_chapter._effects.Length];
                for (int i = 0; i < _chapter._effects.Length; i++)
                {
                    textures[i] = _chapter._effects[i]._iconEffect;
                }
            }

            FillPopulatePresetList(textures, "Effect", 50f, 50f, SelectEffect);
        }
    }
    
    private void FillPopulatePresetList(Texture[] textures, string nameList, float width, float height, Action<EventBase> eventCallback)
    {
        var backgrounds = rootVisualElement.Q<Label>($"{nameList}sLabel");
    
        Texture texture;
        string name;
    
        name = $"{nameList}_{0}";
        texture = null;
    
        backgrounds.Add(CreatePopulatePresetElement(name, width, height, texture, eventCallback));
    
        if (textures != null)
        {
            for (int i = 0; i < textures.Length; i++)
            {
                name = $"{nameList}_{(i + 1)}";
                texture = textures[i];
    
                backgrounds.Add(CreatePopulatePresetElement(name, width, height, texture, eventCallback));
            }
        }
    }
    
    private Button CreatePopulatePresetElement(string name, float width, float height, Texture texture, Action<EventBase> eventCallback)
    {
        Button button = new Button();
        button.name = name;
        button.Add(CreateImageElement("", width, height, texture));
    
        button.clickable.clickedWithEventInfo += eventCallback;
    
        return button;
    }

    private void SearchLevels()
    {
        if(_chapter.CountLevel == 0)
        {
            CreatePresetLevel();
            _currentLevel._levelNumber = 1;
            _currentNumberLevel = 1;
            FullnessEmptyPresetInLevel();
            FillPreset();
        }
        else
        {
            _currentNumberLevel = _chapter.CountLevel;
            OpenLevel();
        }
    }

    private void CreatePresetLevel()
    {
        _currentLevel = new LevelObject();
        _currentLevel._chapter = _chapter;
        FullnessEmptyPresetInLevel();
    }

    private void OpenLevel()
    {
        _currentLevel = LoaderAssets<LevelObject>.GetAsset(string.Format(LEVELS_FILE_PATH, _chapter._numberChapter,_currentNumberLevel));
        if (_currentLevel == null)
        {
            CreatePresetLevel();
            _currentLevel._levelNumber = _currentNumberLevel;
            FullnessEmptyPresetInLevel();
        }
        
        FillPreset();
    }

    private void FullnessEmptyPresetInLevel()
    {
        _levelSquares = new FieldSquare[_countgroundX, _countGroundY];
        for(int i = 0; i < _countgroundX; i++)
        {
            for(int j = 0; j < _countGroundY; j++)
            { 
                FieldSquare fieldSquare = new FieldSquare();
    
                fieldSquare.Row = i;
                fieldSquare.Colum = j;
                fieldSquare.BlockNumber = 0;
                fieldSquare.EffectNumber = 0;
    
                _levelSquares[i, j] = fieldSquare;
    
                _currentLevel._levelSquares.Add(fieldSquare);                
            }
        }
    }

    private void FillPreset()
    {
        _currentNumberLevel = _currentLevel._levelNumber;
        rootVisualElement.Q<TextField>("Level").value = _currentNumberLevel.ToString();
        
        FillArrayPreset();
        
        var preset = rootVisualElement.Q<VisualElement>("ListView");
        preset.Clear();
        for (int i = 0; i < _countgroundX; i++)
        {
            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            for (int j = 0; j < _countGroundY; j++)
            {
                Texture imageBlock;
                Texture imageEffect;
                
                if(_levelSquares[i,j].BlockNumber != 0)
                {
                    imageBlock = _chapter._blocks[_levelSquares[i,j].BlockNumber - 1]._iconBlock;
                }
                else
                {
                    imageBlock = null;
                }
                if(_levelSquares[i,j].EffectNumber != 0)
                {
                    imageEffect = _chapter._effects[_levelSquares[i,j].EffectNumber]._iconEffect;
                }
                else
                {
                    imageEffect = null;
                }
                CreatePresetElement(row, i, j, imageBlock, imageEffect);
            }
            preset.Add(row);
        }     
    }
    
    private void CreatePresetElement(VisualElement parentVisualElement, int row, int column, Texture textureBlock, Texture textureEffect)
    {
        Button buttonPreset = new Button();
        buttonPreset.name = $"{row}|{column}";

        Image imageBlock = CreateImageElement($"{row}|{column} BlockImage", 50.0f, 20.0f, textureBlock);

        buttonPreset.Add(imageBlock);

        imageBlock.Add(CreateImageElement($"{row}|{column} EffectImage", 20.0f, 20.0f, textureEffect));

        buttonPreset.clickable.clickedWithEventInfo += OnUpdatePreset;

        parentVisualElement.Add(buttonPreset);
    }    

    private void FillArrayPreset()
    {
        _levelSquares = new FieldSquare[_countgroundX, _countGroundY];
        
        foreach (FieldSquare fieldSquare in _currentLevel._levelSquares)
        {
            _levelSquares[fieldSquare.Row, fieldSquare.Colum] = fieldSquare;
        }
    }
    
    private Image CreateImageElement(string name, float width, float height, Texture texture)
    {
        Image image = new Image();
        image.name = name;
        image.style.width = width;
        image.style.height = height;
        image.image = texture;

        return image;
    }

    private void SelectBackground(EventBase element)
    {
        VisualElement background = (VisualElement)element.target;
        _countBackground = int.Parse(background.name.Split('_')[1]);
        _currentLevel._background = _countBackground - 1;
        
    }
    
    private void SelectBlock(EventBase element)
    {
        VisualElement block = (VisualElement)element.target;
        _countBlock = int.Parse(block.name.Split('_')[1]);
        _countEffect = -1;
        
    }
    
    private void SelectEffect(EventBase element)
    {
        VisualElement effect = (VisualElement)element.target;
        _countEffect = int.Parse(effect.name.Split('_')[1]);
        _countBlock = -1;
    }
    
    private void OnUpdatePreset(EventBase element)
    {
        VisualElement preset = (VisualElement)element.target;
        string[] index = preset.name.Split('|');
        int indexI = int.Parse(index[0]);
        int indexJ = int.Parse(index[1]);

        string name = string.Empty;
        int count = 0;
        Texture texture = null;

        if(_countBlock != -1)
        {
            name = $"{preset.name} BlockImage";
            count = _countBlock;
            if (count > 0)
            {
                texture = _chapter._blocks[_countBlock - 1]._iconBlock;
            }

            _levelSquares[indexI, indexJ].BlockNumber = _countBlock;
        }
        if(_countEffect != -1)
        {
            name = $"{preset.name} EffectImage";
            count = _countEffect;
            if (count > 0)
            {
                texture = _chapter._effects[_countEffect - 1]._iconEffect;
            }

            _levelSquares[indexI, indexJ].EffectNumber = _countEffect;
        }

        ChangeImagePreset(name, count, texture);
    }

    private void ChangeImagePreset(string name, int count, Texture texture)
    {
        Image image = rootVisualElement.Q<Image>(name);
        if (count == 0)
        {
            image.image = null;
        }
        else
        {
            image.image = texture;
        }
    }

    private void OnSavePreset()
    {
        if (LoaderAssets<LevelObject>.GetAsset(string.Format(LEVELS_FILE_PATH, _chapter._numberChapter, _currentNumberLevel)) == null)
        {
            SavePressetForAsset();
            _chapter.CountLevel++;
            LoaderAssets<LevelObject>.CreateAsset(_currentLevel, string.Format(LEVELS_FILE_PATH, _chapter._numberChapter, _currentNumberLevel));
        }
    }

    private void SavePressetForAsset()
    {
        _currentLevel._levelSquares = new List<FieldSquare>();
        for (int i = 0; i < _countgroundX; i++)
        {
            for(int j = 0; j < _countGroundY; j++)
            {
                _currentLevel._levelSquares.Add(_levelSquares[i, j]);
            }
        }
    }

    private void OnDelitePreset()
    {
        if (LoaderAssets<LevelObject>.GetAsset(string.Format(LEVELS_FILE_PATH, _chapter._numberChapter, _currentNumberLevel)) != null)
        {
            LoaderAssets<LevelObject>.DeliteAsset(string.Format(LEVELS_FILE_PATH, _chapter._numberChapter, _currentNumberLevel));
            _chapter.CountLevel--;
            _currentNumberLevel--;
            if (_currentNumberLevel > 0)
            {
                OpenLevel();
            }
            else
            {
                SearchLevels();
            }
        }
    }
}