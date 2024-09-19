using Refit;

public interface IBlogApi
{
    [Get("/posts/{id}")]
    Task<Post> GetPostAsync(int id);

    [Get("/posts")]
    Task<List<Post>> GetPostsAsync(int? userId);

    [Post("/posts")]
    Task<Post> CreatePostAsync([Body] Post post);

    [Put("/posts/{id}")]
    Task<Post> UpdatePostAsync(int id, [Body] Post post);

    [Delete("/posts/{id}")]
    Task DeletePostAsync(int id);

    [Get("/posts")]
    Task<List<Post>> GetPostsAsync([Query] PostQueryParameters parameters);
}

public class PostQueryParameters
{
    public int? UserId { get; set; }
    public string? Title { get; set; }
}
