using System;
using NodeEditorFramework;
using Plugins;
using Color = UnityEngine.Color;

public class PrevTaskTargetType : ValueConnectionType //: IConnectionTypeDeclaration
{
    public override string Identifier => "PrevNodeDatas";

    public override Type Type => typeof(TaskTargetNodeBase);

    public override Color Color => Color.yellow;
}

public class NextTaskTargetType : ValueConnectionType // : IConnectionTypeDeclaration
{
    public override string Identifier => "NextNodeDatas";

    public override Type Type => typeof(TaskTargetNodeBase);

    public override Color Color => Color.cyan;
}
