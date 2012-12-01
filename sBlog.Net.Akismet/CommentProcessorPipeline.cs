#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion

using System;
using sBlog.Net.Akismet.Entities;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Akismet.Interfaces;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Akismet
{
    public class CommentProcessorPipeline
    {
        private readonly IComment _commentRepository;
        private readonly ISettings _settingsRepository;
        private readonly IAkismetService _akismetService;
        private readonly IError _error;
        private readonly CommentEntity _commentEntity;
        private readonly RequestData _requestData;

        public CommentProcessorPipeline(IComment commentRepository, ISettings settingsRepository, IAkismetService akismetService, IError error, CommentEntity commentEntity, RequestData requestData)
        {
            _commentRepository = commentRepository;
            _settingsRepository = settingsRepository;
            _akismetService = akismetService;
            _error = error;
            _commentEntity = commentEntity;
            _requestData = requestData;
        }

        public AkismetStatus ProcessComment()
        {
            var akismetEnabled = _settingsRepository.BlogAkismetEnabled;
            var deleteSpam = _settingsRepository.BlogAkismetDeleteSpam;
            var akismetStatus = new AkismetStatus();

            if (akismetEnabled && !_requestData.IsAuthenticated)
            {
                /* If the url or akismet key is invalid, this part
                 * might throw an exception. If it does, the comment 
                 * is swallowed, as we do not know the status of the
                 * comment. If it happens, just correct the url or the
                 * key and you should be fine!
                 * 
                 * */

                var comment = AkismetComment.Create(_commentEntity, _requestData);
                var akistmetPipeline = new AkismetPipeline(_akismetService);
                
                try
                {
                    akismetStatus = akistmetPipeline.Process(comment, _settingsRepository);

                    if (akismetStatus.IsSpam && !deleteSpam)
                    {
                        _commentEntity.CommentStatus = 2; // its spam
                        _commentRepository.AddComment(_commentEntity);
                    }
                    else if (akismetStatus.IsHam)
                    {
                        _commentEntity.CommentStatus = 0;
                        _commentRepository.AddComment(_commentEntity);
                    }
                }
                catch (Exception exception)
                {
                    _error.InsertException(exception);
                }
            }
            else
            {
                // submit the comment, mark as approved if the user is logged in
                _commentEntity.CommentStatus = _requestData.IsAuthenticated ? 0 : 1;
                _commentRepository.AddComment(_commentEntity);
            }

            return akismetStatus;
        }
    }
}
