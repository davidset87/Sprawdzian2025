using System;
using System.Collections.Generic;
using Zestaw3.Models.Games;

namespace Zestaw3.Models.Games;

public partial class Publisher
{
    public int Id { get; set; }

    public string? PublisherName { get; set; }

    public virtual ICollection<GamePublisher> GamePublishers { get; set; } = new List<GamePublisher>();
}
