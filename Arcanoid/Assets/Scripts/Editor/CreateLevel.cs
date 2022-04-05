using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

public class CreateLevel : EditorWindow
{
    private ChapterObject _chapter;

    private LevelObject _currentLevel;
    private int _currentNumberLevel = 0;

    private int _countBackground = 0;
    private int _countBlock = -1;
    private int _countEffect = -1;

    private int _minCountGroung = 6;
    private int _countgroundX = 8;
    private int _countGroundY = 8;
    private int _maxCountGround = 10;

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

        //Preset Menu
        var dimension = root.Q<Vector2Field>("DimensionsField");
        dimension.value = new Vector2(_countgroundX, _countGroundY);

        //CreatePresetInWindows();
    }

    private void CreatePresetElement(VisualElement parentVisualElement, int row, int column, Texture textureBlock, Texture textureEffect)
    {
        Button buttonPreset = new Button();
        buttonPreset.name = $"{row}|{column}";

        Image imageBlock = new Image();
        imageBlock.name = $"{row}|{column} BlockImage";
        imageBlock.style.width = 50.0f;
        imageBlock.style.height = 20.0f;
        imageBlock.image = textureBlock;
        
        buttonPreset.Add(imageBlock);

        Image imageEffect = new Image();
        imageEffect.name = $"{row}|{column} EffectImage";
        imageEffect.style.width = 20.0f;
        imageEffect.style.height = 20.0f;
        imageEffect.image = textureEffect;
        
        imageBlock.Add(imageEffect);

        buttonPreset.clickable.clickedWithEventInfo += OnUpdatePreset;

        parentVisualElement.Add(buttonPreset);
    }

    private void PopulatePresetList()
    {
        if(_chapter != null)
        {
            var backgrounds = rootVisualElement.Q<Label>("BackgroundsLabel");

            Button button = new Button();
            button.name = "Background_" + 0;
            Image image = new Image();
            image.style.width = 50f;
            image.style.height = 50f;
            image.image = null;
            button.Add(image);

            button.clickable.clickedWithEventInfo += SelectBlock;

            backgrounds.Add(button);

            if (_chapter._backgrounds != null)
            {
                for (int i = 0; i < _chapter._backgrounds.Length; i++)
                {
                    button = new Button();
                    button.name = "Background_" + (i + 1);
                    image = new Image();
                    image.style.width = 50f;
                    image.style.height = 50f;
                    image.image = _chapter._backgrounds[i];
                    button.Add(image);

                    button.clickable.clickedWithEventInfo += SelectBackground;

                    backgrounds.Add(button);
                }
            }

            var blocks = rootVisualElement.Q<Label>("BlocksLabel");

            button = new Button();
            button.name = "Block_" + 0;
            image = new Image();
            image.style.width = 50f;
            image.style.height = 50f;
            image.image = null;
            button.Add(image);

            button.clickable.clickedWithEventInfo += SelectBlock;

            blocks.Add(button);

            if (_chapter._blocks != null)
            {
                for (int i = 0; i < _chapter._blocks.Length; i++)
                {
                    button = new Button();
                    button.name = "Block_" + (i + 1);
                    image = new Image();
                    image.style.width = 50f;
                    image.style.height = 50f;
                    image.image = _chapter._blocks[i]._iconBlock;
                    button.Add(image);

                    button.clickable.clickedWithEventInfo += SelectBlock;

                    blocks.Add(button);
                }
            }

            var effects = rootVisualElement.Q<Label>("EffectsLabel");

            button = new Button();
            button.name = "Effeckt_" + 0;
            image = new Image();
            image.style.width = 50f;
            image.style.height = 50f;
            image.image = null;
            button.Add(image);

            button.clickable.clickedWithEventInfo += SelectEffect;

            effects.Add(button);

            if (_chapter._effects != null)
            {
                for (int i = 0; i < _chapter._effects.Length; i++)
                {
                    button = new Button();
                    button.name = "Effeckt_" + (i + 1);
                    image = new Image();
                    image.style.width = 50f;
                    image.style.height = 50f;
                    image.image = _chapter._effects[i]._iconEffect;
                    button.Add(image);

                    button.clickable.clickedWithEventInfo += SelectEffect;

                    effects.Add(button);
                }
            }
        }
    }

    private void SearchLevels()
    {
        if(_chapter.CountLevel == 0)
        {
            _currentLevel = new LevelObject();
            _currentLevel._levelNumber = 1;
            _currentLevel._chapter = _chapter;
            _currentNumberLevel = 1;
            Debug.Log(_currentLevel);
            FullnessEmptyPresetInLevel();
            FillPreset();
        }
        else
        {
            _currentNumberLevel = _chapter.CountLevel;
            OpenLevel();
        }
    }

    private void OpenLevel()
    {
        _currentLevel = LoaderAssets<LevelObject>.GetAsset(string.Format(LEVELS_FILE_PATH, _chapter._numberChapter,_currentNumberLevel));
        if (_currentLevel == null)
        {
            FullnessEmptyPresetInLevel();
        }
        FillPreset();
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

    //WorkToPreset
    private void FullnessEmptyPresetInLevel()
    {
        _currentLevel = new LevelObject();
        _currentLevel._chapter = _chapter;
        _currentLevel._levelNumber = _currentNumberLevel;
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
        
        _levelSquares = new FieldSquare[_countgroundX, _countGroundY];

        foreach (FieldSquare fieldSquare in _currentLevel._levelSquares)
        {
            _levelSquares[fieldSquare.Row, fieldSquare.Colum] = fieldSquare;
        }
        
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

    private void OnUpdatePreset(EventBase element)
    {
        VisualElement preset = (VisualElement)element.target;
        string[] index = preset.name.Split('|');
        int indexI = int.Parse(index[0]);
        int indexJ = int.Parse(index[1]);

        if(_countBlock != -1)
        {
            AddBlockForPreset(indexI, indexJ);
        }
        if(_countEffect != -1)
        {
            AddEffectForPreset(indexI, indexJ);
        }
    }

    private void AddBlockForPreset(int i, int j)
    {
        Image image = rootVisualElement.Q<Image>($"{i}|{j} BlockImage");
        if (_countBlock == 0)
        {
            image.image = null;
        }
        else
        {
            image.image = _chapter._blocks[_countBlock - 1]._iconBlock;
        }

        _levelSquares[i, j].BlockNumber = _countBlock;
        
    }

    private void AddEffectForPreset(int i, int j)
    {
        Image image = rootVisualElement.Q<Image>($"{i}|{j} EffectImage");
        if (_countEffect == 0)
        {
            image.image = null;
        }
        else
        {
            image.image = _chapter._effects[_countEffect - 1]._iconEffect;
        }

        _levelSquares[i, j].EffectNumber = _countEffect;
    }

    private void OnResetPreset()
    {
        FullnessEmptyPresetInLevel();
        FillPreset();
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
        if (LoaderAssets<LevelObject>.GetAsset(string.Format(LEVELS_FILE_PATH, _chapter._numberChapter, _currentNumberLevel)) == null)
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