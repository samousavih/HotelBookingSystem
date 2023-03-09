using System;
public readonly struct Either<L, R>
{
    public readonly static Either<L, R> Bottom = new Either<L, R>();

    internal readonly R right;
    internal readonly L left;
    private readonly bool isRight;
    private readonly bool isLeft;

    private Either(R right)
    {
        this.isRight = true;
        this.isLeft = false;
        this.right = right;
        this.left = default(L);
    }
    private Either(L left)
    {
        this.isLeft = true;
        this.isRight = false;
        this.left = left;
        this.right = default(R);
    }

    public static implicit operator Either<L, R>(R value) =>
        new Either<L, R>(value);

    public static implicit operator Either<L, R>(L value) =>
        new Either<L, R>(value);

    public Either<L, U> Map<U>(Func<R, U> map) =>
        this switch
        {
            { isRight: true } => new Either<L, U>(map(right)),
            { isLeft: true } => new Either<L, U>(left),
        };


    public Either<L, U> Bind<U>(Func<R, Either<L, U>> f) =>
        this switch
        {
            { isRight: true } => f(right),
            { isLeft: true } => new Either<L, U>(left),
        };
}

public static class EitherExtension
{
    public static Either<L, V> SelectMany<U, V, L, R>(this Either<L,R> first, Func<R, Either<L, U>> getSecond, Func<R, U, V> project)
    {
        return first.Bind(a => getSecond(a).Map(b => project(a, b)));
    }
    public static Either<L, U> Select<U,L,R>(this Either<L,R> first, Func<R, U> map) => first.Map(map);
}