using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

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

    private VisualElement[,] _preset;

    [MenuItem("Window/UI Toolkit/CreateLevel")]
    public static void ShowExample()
    {
        CreateLevel wnd = GetWindow<CreateLevel>();
        wnd.titleContent = new GUIContent("CreateLevel");
    }

    private void OnEnable()
    {
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/CreateLevel.uxml");
        VisualElement uxmlRoot = visualTree.CloneTree();
        root.Add(uxmlRoot);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/CreateLevel.uss");

        var preMadeStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/CreateLevel.uss");

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

        decrementButton.clickable.clicked += () =>
        {
            if (_chapter != null && _currentNumberLevel >= 1)
            {
                _currentNumberLevel--;
                OpenLevel();
            }            
        };

        incrementButton.clickable.clicked += () =>
        {
            if (_chapter != null && _currentNumberLevel < _chapter.CountLevel)
            {
                _currentNumberLevel++;
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

        CreatePresetInWindows();
    }

    private void CreatePresetInWindows()
    {
        var preset = rootVisualElement.Q<VisualElement>("LeftPanel");

        _preset = new VisualElement[_countgroundX, _countGroundY];
        for(int i = 0; i < _countgroundX; i++)
        {
            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            for(int j = 0; j < _countGroundY; j++)
            {
                Button buttonPreset = new Button();
                buttonPreset.name = $"{i}|{j}";
                _preset[i, j] = buttonPreset;

                Image imageBlock = new Image();
                imageBlock.name = $"{i}|{j} BlockImage";

                buttonPreset.Add(imageBlock);

                Image imageEffect = new Image();
                imageEffect.name = $"{i}|{j} EffectImage";

                imageBlock.Add(imageEffect);

                buttonPreset.clickable.clickedWithEventInfo += OnUpdatePreset;

                row.Add(buttonPreset);
            }
            preset.Add(row);
        }
        
    }

    private void PopulatePresetList()
    {
        if(_chapter != null)
        {
            var backgrounds = rootVisualElement.Q<Label>("BackgroundsLabel");
            if (_chapter._backgrounds != null)
            {
                for (int i = 0; i < _chapter._backgrounds.Length; i++)
                {
                    Button button = new Button();
                    button.name = "Background_" + i;
                    Image image = new Image();
                    image.image = _chapter._backgrounds[i];
                    button.Add(image);

                    button.clickable.clickedWithEventInfo += SelectBackground;

                    backgrounds.Add(button);
                }
            }

            var blocks = rootVisualElement.Q<Label>("BlocksLabel");
            if (_chapter._backgrounds != null)
            {
                for (int i = 0; i < _chapter._blocks.Length; i++)
                {
                    Button button = new Button();
                    button.name = "Block_" + i;
                    Image image = new Image();
                    image.image = _chapter._blocks[i]._iconBlock;
                    button.Add(image);

                    button.clickable.clickedWithEventInfo += SelectBlock;

                    backgrounds.Add(button);
                }
            }

            var effects = rootVisualElement.Q<Label>("EffectsLabel");
            if (_chapter._backgrounds != null)
            {
                for (int i = 0; i < _chapter._effects.Length; i++)
                {
                    Button button = new Button();
                    button.name = "Effeckt_" + i;
                    Image image = new Image();
                    image.image = _chapter._effects[i]._iconEffect;
                    button.Add(image);

                    button.clickable.clickedWithEventInfo += SelectEffect;

                    backgrounds.Add(button);
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
            CreateNewPresetInLevel();
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
        _currentLevel = Resources.Load<LevelObject>($"Assets/Resources/Chapter{_chapter._numberChapter}/Level{_currentNumberLevel}.asset");
        FillPreset();
    }

    private void SelectBackground(EventBase element)
    {
        VisualElement background = (VisualElement)element.target;
        _countBackground = int.Parse(background.name.Split('_')[1]); ;
        _currentLevel._background = _countBackground;
    }

    private void SelectBlock(EventBase element)
    {
        VisualElement block = (VisualElement)element.target;
        _countBlock = int.Parse(block.name.Split('_')[1]); ;
        _countEffect = -1;
    }

    private void SelectEffect(EventBase element)
    {
        VisualElement effect = (VisualElement)element.target;
        _countEffect = int.Parse(effect.name.Split('_')[1]);
        _countBlock = -1;
    }

    //WorkToPreset
    private void CreateNewPresetInLevel()
    {
        _currentLevel._presetBlock = new string[_countgroundX, _countGroundY];
        for(int i = 0; i < _countgroundX; i++)
        {
            for(int j = 0; j < _countGroundY; j++)
            {
                _currentLevel._presetBlock[i,j] = "0|0";
            }
        }
    }

    private void FillPreset()
    {
        for(int i = 0; i < _countgroundX; i++)
        {
            for(int j = 0; j < _countGroundY; j++)
            {
                string[] preset = _currentLevel._presetBlock[i, j].Split('|');
                var imageBlock = rootVisualElement.Q<Image>($"{i}|{j} BlockImage");
                var imageEffect = rootVisualElement.Q<Image>($"{i}|{j} EffectImage");

                if(preset[0] != "0")
                {                    
                    imageBlock.image = _chapter._blocks[int.Parse(preset[0])]._iconBlock;
                }
                else
                {
                    imageBlock.image = null;
                }
                if(preset[1] != "0")
                {                    
                    imageEffect.image = _chapter._effects[int.Parse(preset[1])]._iconEffect;
                }
                else
                {
                    imageBlock.image = null;
                }
            }
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
        Debug.Log(_chapter._blocks[_countBlock]._iconBlock);
        image.image = _chapter._blocks[_countBlock]._iconBlock;
        string presetData = _currentLevel._presetBlock[i, j];
        presetData = $"{_countBlock}|" + presetData.Split('|')[1];
        _currentLevel._presetBlock[i, j] = presetData;
        
    }

    private void AddEffectForPreset(int i, int j)
    {
        Image image = rootVisualElement.Q<Image>($"{i}|{j} EffectImage");
        image.image = _chapter._blocks[_countBlock]._iconBlock;
        string presetData = _currentLevel._presetBlock[i, j];
        presetData =  presetData.Split('|')[0] + $"|{_countBlock}";
        _currentLevel._presetBlock[i, j] = presetData;
    }

    private void OnSavePreset()
    {
        if (Resources.Load<LevelObject>($"Assets/Resources/Chapters/Chapter{_chapter._numberChapter}/Level{_currentNumberLevel}.asset") == null)
        {
            AssetDatabase.CreateAsset(_currentLevel, $"Assets/Resources/Chapters/Chapter{_chapter._numberChapter}/Level{_currentLevel._levelNumber}.asset");
        }
    }

    private void OnDelitePreset()
    {
        if (Resources.Load<LevelObject>($"Assets/Resources/Chapters/Chapter{_chapter._numberChapter}/Level{_currentNumberLevel}.asset") == null)
        {
            AssetDatabase.DeleteAsset($"Assets/Resources/Chapters/Chapter{_chapter._numberChapter}/Level{_currentLevel._levelNumber}.asset");
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