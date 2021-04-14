import React, { useContext, useState } from "react";
import { PostContext } from "../providers/PostProvider";
import { useHistory } from 'react-router-dom';

const PostForm = () => {
    const { addPost } = useContext(PostContext)

    const [post, setPost] = useState({
        title: "",
        caption: "",
        imageUrl: "",
        UserProfileId: 1
    });

    const history = useHistory();

    const handleControlledInputChange = (event) => {
        const newPost = { ...post }
        let selectedVal = event.target.value
        if (event.target.id.includes("Id")) {
            selectedVal = parseInt(selectedVal)
        }
        newPost[event.target.id] = selectedVal
        setPost(newPost)
    }

    const handleClickSavePost = (event) => {
        event.preventDefault()
        addPost(post)
            .then(() => history.push("/"))
    }

    return (
        <form className="PostForm">
            <h2 className="PostForm__title">New Post</h2>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="title">Title:</label>
                    <input type="text" id="title" onChange={handleControlledInputChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Post Title"
                        value={post.Title} />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="caption">Caption:</label>
                    <input type="text" id="caption" onChange={handleControlledInputChange}
                        required autoFocus
                        className="form-control"
                        placeholder="Caption"
                        value={post.Caption} />
                </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="imageUrl">Image URL:</label>
                    <input type="text" id="imageUrl" onChange={handleControlledInputChange}
                        required autoFocus
                        className="form-control"
                        placeholder="URL"
                        value={post.ImageUrl} />
                </div>
            </fieldset>


            <button className="btn btn-primary"
                onClick={handleClickSavePost}>
                Save Post
          </button>
        </form>
    )
};

export default PostForm;