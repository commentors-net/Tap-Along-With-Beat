# Contributing to Tap Along With Beat

Thank you for your interest in contributing to Tap Along With Beat! This document provides guidelines and instructions for contributing to the project.

## ?? Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Coding Standards](#coding-standards)
- [Commit Guidelines](#commit-guidelines)
- [Pull Request Process](#pull-request-process)
- [Testing Guidelines](#testing-guidelines)
- [Documentation](#documentation)

## ?? Code of Conduct

### Our Pledge
- Be respectful and inclusive
- Welcome newcomers and help them learn
- Focus on what is best for the community
- Show empathy towards other community members

### Expected Behavior
- Use welcoming and inclusive language
- Be respectful of differing viewpoints
- Accept constructive criticism gracefully
- Focus on collaboration

## ?? Getting Started

### Prerequisites
1. Unity 2021.3 LTS or later
2. Visual Studio 2022 with Unity workload
3. Git installed and configured
4. GitHub account

### Initial Setup
1. **Fork the repository**
   ```bash
   # Click "Fork" on GitHub
   ```

2. **Clone your fork**
   ```bash
   git clone https://github.com/YOUR-USERNAME/Tap-Along-With-Beat.git
   cd Tap-Along-With-Beat
   ```

3. **Add upstream remote**
   ```bash
   git remote add upstream https://github.com/commentors-net/Tap-Along-With-Beat.git
   ```

4. **Open in Unity**
   - Follow [UNITY_SETUP_CHECKLIST.md](UNITY_SETUP_CHECKLIST.md)
   - Verify all scripts compile

## ?? Development Workflow

### Before Starting Work

1. **Sync with upstream**
   ```bash
   git checkout main
   git fetch upstream
   git merge upstream/main
   git push origin main
   ```

2. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   # or
   git checkout -b bugfix/issue-number-description
   ```

### Branch Naming Convention

- Features: `feature/feature-name`
- Bug fixes: `bugfix/issue-number-description`
- Documentation: `docs/what-you-are-documenting`
- Refactoring: `refactor/what-you-are-refactoring`
- Performance: `perf/what-you-are-improving`

Examples:
- `feature/hold-notes`
- `bugfix/123-audio-sync-issue`
- `docs/beat-map-guide`
- `refactor/input-system`

## ?? Coding Standards

### C# Style Guide

#### Naming Conventions
```csharp
// Classes, Methods, Properties: PascalCase
public class GameManager { }
public void StartGame() { }
public int CurrentScore { get; set; }

// Private fields: _camelCase
private int _currentCombo;
private float _noteSpeed;

// Local variables, parameters: camelCase
int localScore = 0;
void CalculateScore(int baseScore) { }

// Constants: UPPER_SNAKE_CASE
private const int MAX_COMBO = 100;

// Interfaces: IPrefix
public interface ITappable { }

// Events: OnPrefix
public event Action OnScoreChanged;

// Enums: PascalCase (values too)
public enum GameState
{
    MainMenu,
    Playing,
    Paused
}
```

#### Code Structure
```csharp
using UnityEngine;
using System;
using System.Collections.Generic;

namespace TapAlongWithBeat.YourNamespace
{
    /// <summary>
    /// Brief description of the class.
    /// </summary>
    public class YourClass : MonoBehaviour
    {
        // Serialized fields
        [Header("Settings")]
        [SerializeField] private float speed = 5f;
        
        // Public properties
        public int Score { get; private set; }
        
        // Private fields
        private bool isPlaying;
        
        // Unity lifecycle methods (in order)
        private void Awake() { }
        private void Start() { }
        private void Update() { }
        private void OnDestroy() { }
        
        // Public methods
        public void PublicMethod() { }
        
        // Private methods
        private void PrivateMethod() { }
    }
}
```

#### XML Documentation
```csharp
/// <summary>
/// Calculates the final score based on hit quality and combo.
/// </summary>
/// <param name="hitQuality">The quality of the hit (Perfect, Good, OK, Miss)</param>
/// <param name="currentCombo">The current combo multiplier</param>
/// <returns>The calculated score value</returns>
public int CalculateScore(HitQuality hitQuality, int currentCombo)
{
    // Implementation
}
```

### Unity-Specific Guidelines

#### SerializeField vs Public
```csharp
// Prefer SerializeField for Inspector exposure
[SerializeField] private float speed = 5f;

// Use public only when accessed by other scripts
public int CurrentScore => _currentScore;
```

#### Component References
```csharp
// Cache component references
private Rigidbody2D _rigidbody;

private void Awake()
{
    _rigidbody = GetComponent<Rigidbody2D>();
}
```

#### Singleton Pattern
```csharp
// Use provided Singleton base class
public class MyManager : Singleton<MyManager>
{
    protected override void Awake()
    {
        base.Awake(); // Important!
        // Your initialization
    }
}
```

### Performance Best Practices

```csharp
// ? Bad: Creating garbage in Update
void Update()
{
    string message = "Score: " + score.ToString();
}

// ? Good: Cache and reuse
private StringBuilder _scoreBuilder = new StringBuilder();
void Update()
{
    _scoreBuilder.Clear();
    _scoreBuilder.Append("Score: ");
    _scoreBuilder.Append(score);
}

// ? Bad: Repeated GetComponent
void Update()
{
    GetComponent<AudioSource>().Play();
}

// ? Good: Cache in Awake/Start
private AudioSource _audioSource;
void Awake()
{
    _audioSource = GetComponent<AudioSource>();
}
void Update()
{
    _audioSource.Play();
}
```

## ?? Commit Guidelines

### Commit Message Format
```
<type>(<scope>): <subject>

<body>

<footer>
```

### Types
- **feat**: New feature
- **fix**: Bug fix
- **docs**: Documentation changes
- **style**: Code style changes (formatting, etc.)
- **refactor**: Code refactoring
- **perf**: Performance improvements
- **test**: Adding or updating tests
- **chore**: Maintenance tasks

### Examples
```bash
feat(gameplay): add hold note type

- Implement HoldNoteController extending NoteController
- Add hold duration tracking
- Update UI to show hold indicator

Closes #42

---

fix(audio): resolve music synchronization issue

- Fix audio time calculation in AudioManager
- Adjust beat timing window
- Add safety check for null AudioClip

Fixes #38

---

docs(setup): update Unity setup checklist

- Add iOS configuration steps
- Clarify Audio Source settings
- Fix typos in Phase 4
```

### Commit Best Practices
- Write clear, concise commit messages
- Use present tense ("add feature" not "added feature")
- Keep commits atomic (one logical change per commit)
- Reference issues when applicable

## ?? Pull Request Process

### Before Submitting

1. **Test your changes**
   - Test in Unity Editor
   - Build and test on target platform (Android/iOS)
   - Verify no compilation errors
   - Check Console for warnings

2. **Update documentation**
   - Update relevant .md files
   - Add code examples if needed
   - Update comments in code

3. **Review your changes**
   ```bash
   git diff main
   ```

4. **Clean up commits**
   ```bash
   # Squash if needed
   git rebase -i main
   ```

### Creating Pull Request

1. **Push your branch**
   ```bash
   git push origin feature/your-feature-name
   ```

2. **Open Pull Request on GitHub**
   - Use descriptive title
   - Fill out PR template
   - Link related issues

3. **PR Title Format**
   ```
   [Feature] Add hold note type
   [Fix] Resolve audio sync issue #38
   [Docs] Update setup guide
   ```

### PR Template
```markdown
## Description
Brief description of changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Tested in Unity Editor
- [ ] Tested on Android device
- [ ] Tested on iOS device
- [ ] No compilation errors
- [ ] No console warnings

## Related Issues
Closes #issue_number

## Screenshots (if applicable)
Add screenshots or GIFs

## Checklist
- [ ] Code follows style guidelines
- [ ] Comments added where needed
- [ ] Documentation updated
- [ ] No breaking changes (or documented)
```

## ?? Testing Guidelines

### Manual Testing
1. **Editor Testing**
   - Test all changed functionality
   - Check for console errors
   - Verify UI updates correctly

2. **Device Testing**
   - Build APK/IPA
   - Install on device
   - Test touch input
   - Check performance
   - Verify audio synchronization

### What to Test
- ? Core functionality works as expected
- ? No regression (existing features still work)
- ? Performance is acceptable
- ? UI is responsive
- ? Audio plays correctly
- ? Input handling works on mobile

### Testing Checklist
```
- [ ] Feature works as intended
- [ ] No compilation errors
- [ ] No console errors in Editor
- [ ] UI displays correctly
- [ ] Input responds properly
- [ ] Audio syncs correctly
- [ ] Performance is smooth (60 FPS target)
- [ ] Tested on Android device
- [ ] Tested on iOS device (if applicable)
- [ ] No memory leaks observed
```

## ?? Documentation

### When to Update Documentation

Update documentation when:
- Adding new features
- Changing existing functionality
- Adding new scripts
- Modifying project structure
- Fixing bugs that affect usage

### What to Document

1. **Code Documentation**
   - XML comments for public APIs
   - Inline comments for complex logic
   - Clear variable and method names

2. **File Documentation**
   - Update USAGE_EXAMPLES.md with examples
   - Update PROJECT_STRUCTURE.md if structure changes
   - Update SETUP_GUIDE.md if setup changes
   - Add to UNITY_SETUP_CHECKLIST.md if needed

3. **Script Header**
   ```csharp
   /// <summary>
   /// Description of what this script does.
   /// Usage instructions if not obvious.
   /// </summary>
   ```

## ?? Reporting Bugs

### Before Reporting
1. Check existing issues
2. Verify it's reproducible
3. Test on clean project

### Bug Report Template
```markdown
## Bug Description
Clear description of the bug

## Steps to Reproduce
1. Step one
2. Step two
3. Step three

## Expected Behavior
What should happen

## Actual Behavior
What actually happens

## Environment
- Unity Version: 2021.3.x
- Platform: Android/iOS/Editor
- Device: (if mobile)

## Screenshots/Logs
Add any helpful screenshots or console logs

## Possible Solution
(Optional) Suggestions for fixing
```

## ?? Feature Requests

### Feature Request Template
```markdown
## Feature Description
Clear description of the feature

## Problem it Solves
What problem does this solve?

## Proposed Solution
How should it work?

## Alternatives Considered
Other solutions you've thought about

## Additional Context
Any other relevant information
```

## ?? Getting Help

- Check [Documentation](INDEX.md)
- Review [Usage Examples](USAGE_EXAMPLES.md)
- Search existing GitHub issues
- Ask in GitHub Discussions
- Contact maintainers

## ?? Good First Issues

Looking for where to start? Check issues labeled:
- `good first issue`
- `documentation`
- `help wanted`

## ?? Recognition

Contributors will be:
- Listed in CONTRIBUTORS.md
- Credited in release notes
- Thanked in commit messages

## ?? License

By contributing, you agree that your contributions will be licensed under the same license as the project.

---

**Thank you for contributing to Tap Along With Beat! ????**
