using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace HastaneAPP.HastanebilgiLocal
{
    public partial class HastaneBilgiContext : DbContext
    {
        public HastaneBilgiContext()
        {
        }

        public HastaneBilgiContext(DbContextOptions<HastaneBilgiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WpCommentmeta> WpCommentmeta { get; set; }
        public virtual DbSet<WpComments> WpComments { get; set; }
        public virtual DbSet<WpH5pContents> WpH5pContents { get; set; }
        public virtual DbSet<WpH5pContentsLibraries> WpH5pContentsLibraries { get; set; }
        public virtual DbSet<WpH5pContentsTags> WpH5pContentsTags { get; set; }
        public virtual DbSet<WpH5pContentsUserData> WpH5pContentsUserData { get; set; }
        public virtual DbSet<WpH5pCounters> WpH5pCounters { get; set; }
        public virtual DbSet<WpH5pEvents> WpH5pEvents { get; set; }
        public virtual DbSet<WpH5pLibraries> WpH5pLibraries { get; set; }
        public virtual DbSet<WpH5pLibrariesCachedassets> WpH5pLibrariesCachedassets { get; set; }
        public virtual DbSet<WpH5pLibrariesHubCache> WpH5pLibrariesHubCache { get; set; }
        public virtual DbSet<WpH5pLibrariesLanguages> WpH5pLibrariesLanguages { get; set; }
        public virtual DbSet<WpH5pLibrariesLibraries> WpH5pLibrariesLibraries { get; set; }
        public virtual DbSet<WpH5pResults> WpH5pResults { get; set; }
        public virtual DbSet<WpH5pTags> WpH5pTags { get; set; }
        public virtual DbSet<WpH5pTmpfiles> WpH5pTmpfiles { get; set; }
        public virtual DbSet<WpH5pxapikatchu> WpH5pxapikatchu { get; set; }
        public virtual DbSet<WpH5pxapikatchuActor> WpH5pxapikatchuActor { get; set; }
        public virtual DbSet<WpH5pxapikatchuObject> WpH5pxapikatchuObject { get; set; }
        public virtual DbSet<WpH5pxapikatchuResult> WpH5pxapikatchuResult { get; set; }
        public virtual DbSet<WpH5pxapikatchuVerb> WpH5pxapikatchuVerb { get; set; }
        public virtual DbSet<WpLinks> WpLinks { get; set; }
        public virtual DbSet<WpOptions> WpOptions { get; set; }
        public virtual DbSet<WpPostmeta> WpPostmeta { get; set; }
        public virtual DbSet<WpPosts> WpPosts { get; set; }
        public virtual DbSet<WpTermRelationships> WpTermRelationships { get; set; }
        public virtual DbSet<WpTermTaxonomy> WpTermTaxonomy { get; set; }
        public virtual DbSet<WpTermmeta> WpTermmeta { get; set; }
        public virtual DbSet<WpTerms> WpTerms { get; set; }
        public virtual DbSet<WpUsermeta> WpUsermeta { get; set; }
        public virtual DbSet<WpUsers> WpUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();
                    var connectionString = configuration.GetConnectionString("WordpresConnection");
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL(connectionString);
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WpCommentmeta>(entity =>
            {
                entity.HasKey(e => e.MetaId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_commentmeta");

                entity.HasIndex(e => e.CommentId)
                    .HasName("comment_id");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.Property(e => e.MetaId)
                    .HasColumnName("meta_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasMaxLength(255);

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<WpComments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_comments");

                entity.HasIndex(e => e.CommentAuthorEmail)
                    .HasName("comment_author_email");

                entity.HasIndex(e => e.CommentParent)
                    .HasName("comment_parent");

                entity.HasIndex(e => e.CommentPostId)
                    .HasName("comment_post_ID");

                entity.Property(e => e.CommentId)
                    .HasColumnName("comment_ID")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CommentAgent)
                    .IsRequired()
                    .HasColumnName("comment_agent")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentApproved)
                    .IsRequired()
                    .HasColumnName("comment_approved")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.CommentAuthor)
                    .IsRequired()
                    .HasColumnName("comment_author")
                    .HasColumnType("tinytext");

                entity.Property(e => e.CommentAuthorEmail)
                    .IsRequired()
                    .HasColumnName("comment_author_email")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentAuthorIp)
                    .IsRequired()
                    .HasColumnName("comment_author_IP")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentAuthorUrl)
                    .IsRequired()
                    .HasColumnName("comment_author_url")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasColumnName("comment_content");

                entity.Property(e => e.CommentKarma)
                    .HasColumnName("comment_karma")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CommentParent)
                    .HasColumnName("comment_parent")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CommentPostId)
                    .HasColumnName("comment_post_ID")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CommentType)
                    .IsRequired()
                    .HasColumnName("comment_type")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'comment'");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<WpH5pContents>(entity =>
            {
                entity.ToTable("wp_h5p_contents");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AuthorComments)
                    .HasColumnName("author_comments")
                    .HasColumnType("longtext");

                entity.Property(e => e.Authors)
                    .HasColumnName("authors")
                    .HasColumnType("longtext");

                entity.Property(e => e.Changes)
                    .HasColumnName("changes")
                    .HasColumnType("longtext");

                entity.Property(e => e.ContentType)
                    .HasColumnName("content_type")
                    .HasMaxLength(127);

                entity.Property(e => e.DefaultLanguage)
                    .HasColumnName("default_language")
                    .HasMaxLength(32);

                entity.Property(e => e.Disable)
                    .HasColumnName("disable")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.EmbedType)
                    .IsRequired()
                    .HasColumnName("embed_type")
                    .HasMaxLength(127);

                entity.Property(e => e.Filtered)
                    .IsRequired()
                    .HasColumnName("filtered")
                    .HasColumnType("longtext");

                entity.Property(e => e.LibraryId)
                    .HasColumnName("library_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.License)
                    .HasColumnName("license")
                    .HasMaxLength(32);

                entity.Property(e => e.LicenseExtras)
                    .HasColumnName("license_extras")
                    .HasColumnType("longtext");

                entity.Property(e => e.LicenseVersion)
                    .HasColumnName("license_version")
                    .HasMaxLength(10);

                entity.Property(e => e.Parameters)
                    .IsRequired()
                    .HasColumnName("parameters")
                    .HasColumnType("longtext");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasMaxLength(127);

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasMaxLength(2083);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.YearFrom)
                    .HasColumnName("year_from")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.YearTo)
                    .HasColumnName("year_to")
                    .HasColumnType("int(10) unsigned");

            });

            modelBuilder.Entity<WpH5pContentsLibraries>(entity =>
            {
                entity.HasKey(e => new { e.ContentId, e.LibraryId, e.DependencyType })
                    .HasName("PRIMARY");

                entity.ToTable("wp_h5p_contents_libraries");

                entity.Property(e => e.ContentId)
                    .HasColumnName("content_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LibraryId)
                    .HasColumnName("library_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DependencyType)
                    .HasColumnName("dependency_type")
                    .HasMaxLength(31);

                entity.Property(e => e.DropCss)
                    .HasColumnName("drop_css")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasColumnType("smallint(5) unsigned");
            });

            modelBuilder.Entity<WpH5pContentsTags>(entity =>
            {
                entity.HasKey(e => new { e.ContentId, e.TagId })
                    .HasName("PRIMARY");

                entity.ToTable("wp_h5p_contents_tags");

                entity.Property(e => e.ContentId)
                    .HasColumnName("content_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.TagId)
                    .HasColumnName("tag_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<WpH5pContentsUserData>(entity =>
            {
                entity.HasKey(e => new { e.ContentId, e.UserId, e.SubContentId, e.DataId })
                    .HasName("PRIMARY");

                entity.ToTable("wp_h5p_contents_user_data");

                entity.Property(e => e.ContentId)
                    .HasColumnName("content_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.SubContentId)
                    .HasColumnName("sub_content_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DataId)
                    .HasColumnName("data_id")
                    .HasMaxLength(127);

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasColumnType("longtext");

                entity.Property(e => e.Invalidate)
                    .HasColumnName("invalidate")
                    .HasColumnType("tinyint(3) unsigned");

                entity.Property(e => e.Preload)
                    .HasColumnName("preload")
                    .HasColumnType("tinyint(3) unsigned");
            });

            modelBuilder.Entity<WpH5pCounters>(entity =>
            {
                entity.HasKey(e => new { e.Type, e.LibraryName, e.LibraryVersion })
                    .HasName("PRIMARY");

                entity.ToTable("wp_h5p_counters");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(63);

                entity.Property(e => e.LibraryName)
                    .HasColumnName("library_name")
                    .HasMaxLength(127);

                entity.Property(e => e.LibraryVersion)
                    .HasColumnName("library_version")
                    .HasMaxLength(31);

                entity.Property(e => e.Num)
                    .HasColumnName("num")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<WpH5pEvents>(entity =>
            {
                entity.ToTable("wp_h5p_events");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ContentId)
                    .HasColumnName("content_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ContentTitle)
                    .IsRequired()
                    .HasColumnName("content_title")
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LibraryName)
                    .IsRequired()
                    .HasColumnName("library_name")
                    .HasMaxLength(127);

                entity.Property(e => e.LibraryVersion)
                    .IsRequired()
                    .HasColumnName("library_version")
                    .HasMaxLength(31);

                entity.Property(e => e.SubType)
                    .IsRequired()
                    .HasColumnName("sub_type")
                    .HasMaxLength(63);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(63);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<WpH5pLibraries>(entity =>
            {
                entity.ToTable("wp_h5p_libraries");

                entity.HasIndex(e => e.Runnable)
                    .HasName("runnable");

                entity.HasIndex(e => new { e.Name, e.MajorVersion, e.MinorVersion, e.PatchVersion })
                    .HasName("name_version");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.AddTo).HasColumnName("add_to");

                entity.Property(e => e.DropLibraryCss).HasColumnName("drop_library_css");

                entity.Property(e => e.EmbedTypes)
                    .IsRequired()
                    .HasColumnName("embed_types")
                    .HasMaxLength(255);

                entity.Property(e => e.Fullscreen)
                    .HasColumnName("fullscreen")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.HasIcon)
                    .HasColumnName("has_icon")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MajorVersion)
                    .HasColumnName("major_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MetadataSettings).HasColumnName("metadata_settings");

                entity.Property(e => e.MinorVersion)
                    .HasColumnName("minor_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(127);

                entity.Property(e => e.PatchVersion)
                    .HasColumnName("patch_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.PreloadedCss).HasColumnName("preloaded_css");

                entity.Property(e => e.PreloadedJs).HasColumnName("preloaded_js");

                entity.Property(e => e.Restricted)
                    .HasColumnName("restricted")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Runnable)
                    .HasColumnName("runnable")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Semantics)
                    .IsRequired()
                    .HasColumnName("semantics");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.TutorialUrl)
                    .IsRequired()
                    .HasColumnName("tutorial_url")
                    .HasMaxLength(1023);
            });

            modelBuilder.Entity<WpH5pLibrariesCachedassets>(entity =>
            {
                entity.HasKey(e => new { e.LibraryId, e.Hash })
                    .HasName("PRIMARY");

                entity.ToTable("wp_h5p_libraries_cachedassets");

                entity.Property(e => e.LibraryId)
                    .HasColumnName("library_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Hash)
                    .HasColumnName("hash")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<WpH5pLibrariesHubCache>(entity =>
            {
                entity.ToTable("wp_h5p_libraries_hub_cache");

                entity.HasIndex(e => new { e.MachineName, e.MajorVersion, e.MinorVersion, e.PatchVersion })
                    .HasName("name_version");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Categories).HasColumnName("categories");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Example)
                    .IsRequired()
                    .HasColumnName("example")
                    .HasMaxLength(511);

                entity.Property(e => e.H5pMajorVersion)
                    .HasColumnName("h5p_major_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.H5pMinorVersion)
                    .HasColumnName("h5p_minor_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasColumnName("icon")
                    .HasMaxLength(511);

                entity.Property(e => e.IsRecommended)
                    .HasColumnName("is_recommended")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Keywords).HasColumnName("keywords");

                entity.Property(e => e.License).HasColumnName("license");

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasColumnName("machine_name")
                    .HasMaxLength(127);

                entity.Property(e => e.MajorVersion)
                    .HasColumnName("major_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MinorVersion)
                    .HasColumnName("minor_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasMaxLength(511);

                entity.Property(e => e.PatchVersion)
                    .HasColumnName("patch_version")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Popularity)
                    .HasColumnName("popularity")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Screenshots).HasColumnName("screenshots");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasColumnName("summary");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.Tutorial)
                    .HasColumnName("tutorial")
                    .HasMaxLength(511);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<WpH5pLibrariesLanguages>(entity =>
            {
                entity.HasKey(e => new { e.LibraryId, e.LanguageCode })
                    .HasName("PRIMARY");

                entity.ToTable("wp_h5p_libraries_languages");

                entity.Property(e => e.LibraryId)
                    .HasColumnName("library_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.LanguageCode)
                    .HasColumnName("language_code")
                    .HasMaxLength(31);

                entity.Property(e => e.Translation)
                    .IsRequired()
                    .HasColumnName("translation");
            });

            modelBuilder.Entity<WpH5pLibrariesLibraries>(entity =>
            {
                entity.HasKey(e => new { e.LibraryId, e.RequiredLibraryId })
                    .HasName("PRIMARY");

                entity.ToTable("wp_h5p_libraries_libraries");

                entity.Property(e => e.LibraryId)
                    .HasColumnName("library_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.RequiredLibraryId)
                    .HasColumnName("required_library_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.DependencyType)
                    .IsRequired()
                    .HasColumnName("dependency_type")
                    .HasMaxLength(31);
            });

            modelBuilder.Entity<WpH5pResults>(entity =>
            {
                entity.ToTable("wp_h5p_results");

                entity.HasIndex(e => new { e.ContentId, e.UserId })
                    .HasName("content_user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.ContentId)
                    .HasColumnName("content_id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Finished)
                    .HasColumnName("finished")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.MaxScore)
                    .HasColumnName("max_score")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Opened)
                    .HasColumnName("opened")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(10) unsigned");
            });

            modelBuilder.Entity<WpH5pTags>(entity =>
            {
                entity.ToTable("wp_h5p_tags");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(31);
            });

            modelBuilder.Entity<WpH5pTmpfiles>(entity =>
            {
                entity.ToTable("wp_h5p_tmpfiles");

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("created_at");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("int(10) unsigned");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<WpH5pxapikatchu>(entity =>
            {
                entity.ToTable("wp_h5pxapikatchu");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.IdActor)
                    .HasColumnName("id_actor")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.IdObject)
                    .HasColumnName("id_object")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.IdResult)
                    .HasColumnName("id_result")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.IdVerb)
                    .HasColumnName("id_verb")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.Xapi).HasColumnName("xapi");
            });

            modelBuilder.Entity<WpH5pxapikatchuActor>(entity =>
            {
                entity.ToTable("wp_h5pxapikatchu_actor");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.ActorMembers).HasColumnName("actor_members");

                entity.Property(e => e.ActorName).HasColumnName("actor_name");

                entity.Property(e => e.WpUserId)
                    .HasColumnName("wp_user_id")
                    .HasColumnType("bigint(20)");
            });

            modelBuilder.Entity<WpH5pxapikatchuObject>(entity =>
            {
                entity.ToTable("wp_h5pxapikatchu_object");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.H5pContentId)
                    .HasColumnName("h5p_content_id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.H5pSubcontentId)
                    .HasColumnName("h5p_subcontent_id")
                    .HasMaxLength(36);

                entity.Property(e => e.ObjectChoices).HasColumnName("object_choices");

                entity.Property(e => e.ObjectCorrectResponsesPattern).HasColumnName("object_correct_responses_pattern");

                entity.Property(e => e.ObjectDescription).HasColumnName("object_description");

                entity.Property(e => e.ObjectName).HasColumnName("object_name");

                entity.Property(e => e.XobjectId).HasColumnName("xobject_id");
            });

            modelBuilder.Entity<WpH5pxapikatchuResult>(entity =>
            {
                entity.ToTable("wp_h5pxapikatchu_result");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.ResultCompletion).HasColumnName("result_completion");

                entity.Property(e => e.ResultDuration)
                    .HasColumnName("result_duration")
                    .HasMaxLength(20);

                entity.Property(e => e.ResultResponse).HasColumnName("result_response");

                entity.Property(e => e.ResultScoreRaw)
                    .HasColumnName("result_score_raw")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ResultScoreScaled).HasColumnName("result_score_scaled");

                entity.Property(e => e.ResultSuccess).HasColumnName("result_success");
            });

            modelBuilder.Entity<WpH5pxapikatchuVerb>(entity =>
            {
                entity.ToTable("wp_h5pxapikatchu_verb");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("mediumint(9)");

                entity.Property(e => e.VerbDisplay).HasColumnName("verb_display");

                entity.Property(e => e.VerbId).HasColumnName("verb_id");
            });

            modelBuilder.Entity<WpLinks>(entity =>
            {
                entity.HasKey(e => e.LinkId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_links");

                entity.HasIndex(e => e.LinkVisible)
                    .HasName("link_visible");

                entity.Property(e => e.LinkId)
                    .HasColumnName("link_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.LinkDescription)
                    .IsRequired()
                    .HasColumnName("link_description")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkImage)
                    .IsRequired()
                    .HasColumnName("link_image")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkName)
                    .IsRequired()
                    .HasColumnName("link_name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkNotes)
                    .IsRequired()
                    .HasColumnName("link_notes")
                    .HasColumnType("mediumtext");

                entity.Property(e => e.LinkOwner)
                    .HasColumnName("link_owner")
                    .HasColumnType("bigint(20) unsigned")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LinkRating)
                    .HasColumnName("link_rating")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LinkRel)
                    .IsRequired()
                    .HasColumnName("link_rel")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkRss)
                    .IsRequired()
                    .HasColumnName("link_rss")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkTarget)
                    .IsRequired()
                    .HasColumnName("link_target")
                    .HasMaxLength(25)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkUrl)
                    .IsRequired()
                    .HasColumnName("link_url")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LinkVisible)
                    .IsRequired()
                    .HasColumnName("link_visible")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'Y'");
            });

            modelBuilder.Entity<WpOptions>(entity =>
            {
                entity.HasKey(e => e.OptionId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_options");

                entity.HasIndex(e => e.Autoload)
                    .HasName("autoload");

                entity.HasIndex(e => e.OptionName)
                    .HasName("option_name")
                    .IsUnique();

                entity.Property(e => e.OptionId)
                    .HasColumnName("option_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Autoload)
                    .IsRequired()
                    .HasColumnName("autoload")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'yes'");

                entity.Property(e => e.OptionName)
                    .IsRequired()
                    .HasColumnName("option_name")
                    .HasMaxLength(191)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.OptionValue)
                    .IsRequired()
                    .HasColumnName("option_value")
                    .HasColumnType("longtext");
            });

            modelBuilder.Entity<WpPostmeta>(entity =>
            {
                entity.HasKey(e => e.MetaId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_postmeta");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.HasIndex(e => e.PostId)
                    .HasName("post_id");

                entity.Property(e => e.MetaId)
                    .HasColumnName("meta_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasMaxLength(255);

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");

                entity.Property(e => e.PostId)
                    .HasColumnName("post_id")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<WpPosts>(entity =>
            {
                entity.ToTable("wp_posts");

                entity.HasIndex(e => e.PostAuthor)
                    .HasName("post_author");

                entity.HasIndex(e => e.PostName)
                    .HasName("post_name");

                entity.HasIndex(e => e.PostParent)
                    .HasName("post_parent");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.CommentCount)
                    .HasColumnName("comment_count")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.CommentStatus)
                    .IsRequired()
                    .HasColumnName("comment_status")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'open'");

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasColumnName("guid")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.MenuOrder)
                    .HasColumnName("menu_order")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PingStatus)
                    .IsRequired()
                    .HasColumnName("ping_status")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'open'");

                entity.Property(e => e.Pinged)
                    .IsRequired()
                    .HasColumnName("pinged");

                entity.Property(e => e.PostAuthor)
                    .HasColumnName("post_author")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.PostContent)
                    .IsRequired()
                    .HasColumnName("post_content")
                    .HasColumnType("longtext");

                entity.Property(e => e.PostContentFiltered)
                    .IsRequired()
                    .HasColumnName("post_content_filtered")
                    .HasColumnType("longtext");

                entity.Property(e => e.PostExcerpt)
                    .IsRequired()
                    .HasColumnName("post_excerpt");

                entity.Property(e => e.PostMimeType)
                    .IsRequired()
                    .HasColumnName("post_mime_type")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PostName)
                    .IsRequired()
                    .HasColumnName("post_name")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PostParent)
                    .HasColumnName("post_parent")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.PostPassword)
                    .IsRequired()
                    .HasColumnName("post_password")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PostStatus)
                    .IsRequired()
                    .HasColumnName("post_status")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'publish'");

                entity.Property(e => e.PostTitle)
                    .IsRequired()
                    .HasColumnName("post_title");

                entity.Property(e => e.PostType)
                    .IsRequired()
                    .HasColumnName("post_type")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'post'");

                entity.Property(e => e.ToPing)
                    .IsRequired()
                    .HasColumnName("to_ping");
            });

            modelBuilder.Entity<WpTermRelationships>(entity =>
            {
                entity.HasKey(e => new { e.ObjectId, e.TermTaxonomyId })
                    .HasName("PRIMARY");

                entity.ToTable("wp_term_relationships");

                entity.HasIndex(e => e.TermTaxonomyId)
                    .HasName("term_taxonomy_id");

                entity.Property(e => e.ObjectId)
                    .HasColumnName("object_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TermTaxonomyId)
                    .HasColumnName("term_taxonomy_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.TermOrder)
                    .HasColumnName("term_order")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<WpTermTaxonomy>(entity =>
            {
                entity.HasKey(e => e.TermTaxonomyId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_term_taxonomy");

                entity.HasIndex(e => e.Taxonomy)
                    .HasName("taxonomy");

                entity.HasIndex(e => new { e.TermId, e.Taxonomy })
                    .HasName("term_id_taxonomy")
                    .IsUnique();

                entity.Property(e => e.TermTaxonomyId)
                    .HasColumnName("term_taxonomy_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("longtext");

                entity.Property(e => e.Parent)
                    .HasColumnName("parent")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Taxonomy)
                    .IsRequired()
                    .HasColumnName("taxonomy")
                    .HasMaxLength(32)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.TermId)
                    .HasColumnName("term_id")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<WpTermmeta>(entity =>
            {
                entity.HasKey(e => e.MetaId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_termmeta");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.HasIndex(e => e.TermId)
                    .HasName("term_id");

                entity.Property(e => e.MetaId)
                    .HasColumnName("meta_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasMaxLength(255);

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");

                entity.Property(e => e.TermId)
                    .HasColumnName("term_id")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<WpTerms>(entity =>
            {
                entity.HasKey(e => e.TermId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_terms");

                entity.HasIndex(e => e.Name)
                    .HasName("name");

                entity.HasIndex(e => e.Slug)
                    .HasName("slug");

                entity.Property(e => e.TermId)
                    .HasColumnName("term_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.TermGroup)
                    .HasColumnName("term_group")
                    .HasColumnType("bigint(10)");
            });

            modelBuilder.Entity<WpUsermeta>(entity =>
            {
                entity.HasKey(e => e.UmetaId)
                    .HasName("PRIMARY");

                entity.ToTable("wp_usermeta");

                entity.HasIndex(e => e.MetaKey)
                    .HasName("meta_key");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id");

                entity.Property(e => e.UmetaId)
                    .HasColumnName("umeta_id")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.MetaKey)
                    .HasColumnName("meta_key")
                    .HasMaxLength(255);

                entity.Property(e => e.MetaValue)
                    .HasColumnName("meta_value")
                    .HasColumnType("longtext");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("bigint(20) unsigned");
            });

            modelBuilder.Entity<WpUsers>(entity =>
            {
                entity.ToTable("wp_users");

                entity.HasIndex(e => e.UserEmail)
                    .HasName("user_email");

                entity.HasIndex(e => e.UserLogin)
                    .HasName("user_login_key");

                entity.HasIndex(e => e.UserNicename)
                    .HasName("user_nicename");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("bigint(20) unsigned");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnName("display_name")
                    .HasMaxLength(250)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserActivationKey)
                    .IsRequired()
                    .HasColumnName("user_activation_key")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasColumnName("user_email")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasColumnName("user_login")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserNicename)
                    .IsRequired()
                    .HasColumnName("user_nicename")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserPass)
                    .IsRequired()
                    .HasColumnName("user_pass")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserStatus)
                    .HasColumnName("user_status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserUrl)
                    .IsRequired()
                    .HasColumnName("user_url")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e =>e.Registered)
                    .HasColumnName("user_registered");

                entity.Property(e => e.Operation)
                    .HasColumnName("Operation")
                    .HasColumnType("longtext");

                 entity.Property(e => e.Gender)
                    .HasColumnName("Gender")
                    .HasColumnType("longtext");

                 entity.Property(e => e.Age)
                    .HasColumnName("Age")
                    .HasColumnType("int(3)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
