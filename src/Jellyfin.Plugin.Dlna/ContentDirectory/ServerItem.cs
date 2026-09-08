using System;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Dto;

namespace Jellyfin.Plugin.Dlna.ContentDirectory;

/// <summary>
/// Defines the <see cref="ServerItem" />.
/// </summary>
internal sealed class ServerItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ServerItem"/> class.
    /// </summary>
    /// <param name="item">The <see cref="BaseItem"/>.</param>
    /// <param name="stubType">The stub type.</param>
    /// <param name="partNumber">The one based part number of a stacked (multi-part) video.</param>
    /// <param name="ancestorId">The library the client browsed in from.</param>
    /// <param name="itemCounts">The counts the listing reported for the item, if any.</param>
    public ServerItem(BaseItem item, StubType? stubType, int? partNumber = null, Guid? ancestorId = null, ItemCounts? itemCounts = null)
    {
        Item = item;
        PartNumber = partNumber;
        AncestorId = ancestorId;
        ItemCounts = itemCounts;

        if (stubType.HasValue)
        {
            StubType = stubType;
        }
        else if (item is IItemByName and not Folder)
        {
            StubType = ContentDirectory.StubType.Folder;
        }
    }

    /// <summary>
    /// Gets the underlying base item.
    /// </summary>
    public BaseItem Item { get; }

    /// <summary>
    /// Gets the DLNA item type.
    /// </summary>
    public StubType? StubType { get; }

    /// <summary>
    /// Gets the one based part number when the item is one part of a stacked (multi-part) video.
    /// </summary>
    public int? PartNumber { get; }

    /// <summary>
    /// Gets the library the client browsed in from, for items such as genres and artists whose
    /// content otherwise spans every library.
    /// </summary>
    public Guid? AncestorId { get; }

    /// <summary>
    /// Gets the counts the listing this item came from reported for it, if it reported any. They
    /// carry the scope of that listing, so a genre listed under a library is counted within it.
    /// </summary>
    public ItemCounts? ItemCounts { get; }
}
