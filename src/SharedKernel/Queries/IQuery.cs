using MediatR;

namespace SharedKernel.Queries;

public interface IQuery : IRequest<Result>;
public interface IQuery<TResult> : IRequest<Result<TResult>>;
