// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Matrix = Microsoft.Xna.Framework.Matrix;
using Microsoft.Xna.Framework;

namespace ModelViewer
{
    public class XnaModelWrapper
    {
        #region Fields
        // The XNA framework Model object that we are going to display.
        Model xnaModel;

        // Array holding all the bone transform matrices for the entire model.
        // We could just allocate this locally inside the Draw method, but it
        // is more efficient to reuse a single array, as this avoids creating
        // unnecessary garbage.
        Matrix[] boneTransforms;

        #endregion

        #region Initialization
        /// <summary>
        /// Loads the model.
        /// </summary>
        public void Load(ContentManager content, string modelName)
        {
            // Load the model from the ContentManager.
            xnaModel = content.Load<Model>(modelName);

            // Allocate the transform matrix array.
            boneTransforms = new Matrix[xnaModel.Bones.Count];
        }
        #endregion

        #region Public accessors

        /// <summary>
        /// Gets or sets the projection matrix value.
        /// </summary>
        public Matrix Projection { get; set; }

        /// <summary>
        /// Gets or sets the rotation matrix value.
        /// </summary>
        public Matrix Rotation { get; set; }

        /// <summary>
        /// Gets or sets the rotation matrix value.
        /// </summary>
        public bool IsTextureEnabled { get; set; }

        /// <summary>
        /// Gets or sets the view matrix value.
        /// </summary>
        public Matrix View { get; set; }

        /// <summary>
        /// Gets or sets the lights states.
        /// </summary>
        public bool[] Lights { get; set; }

        /// <summary>
        /// Gets or sets the per pixel lighting preferences
        /// </summary>
        public bool IsPerPixelLightingEnabled { get; set; }

        #endregion

        #region Draw
        /// <summary>
        /// Draws the model, using the current drawing parameters.
        /// </summary>
        public void Draw()
        {
            // Set the world matrix as the root transform of the model.
            xnaModel.Root.Transform = Rotation;

            // Look up combined bone matrices for the entire model.
            xnaModel.CopyAbsoluteBoneTransformsTo(boneTransforms);

            // Draw the model.
            foreach (ModelMesh mesh in xnaModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index];
                    effect.View = View;
                    effect.Projection = Projection;

                    SetEffectLights(effect, Lights);
                    SetEffectPerPixelLightingEnabled(effect);

                    effect.TextureEnabled = IsTextureEnabled;
                }

                mesh.Draw();
            }
        }

        /// <summary>
        /// Sets effect's per pixel lighting preference
        /// </summary>
        /// <param name="effect"></param>
        private void SetEffectPerPixelLightingEnabled(BasicEffect effect)
        {
            effect.PreferPerPixelLighting = IsPerPixelLightingEnabled;
        }

        /// <summary>
        /// Sets effects lighting properties
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="lights"></param>
        private void SetEffectLights(BasicEffect effect, bool[] lights)
        {

            effect.Alpha = 1.0f;
            effect.DiffuseColor = new Vector3(0.75f, 0.75f, 0.75f);
            effect.SpecularColor = new Vector3(0.25f, 0.25f, 0.25f);
            effect.SpecularPower = 5.0f;
            effect.AmbientLightColor = new Vector3(0.75f, 0.75f, 0.75f);

            effect.DirectionalLight0.Enabled = lights[0];
            effect.DirectionalLight0.DiffuseColor = Vector3.One;
            effect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(1, -1, 0));
            effect.DirectionalLight0.SpecularColor = Vector3.One;

            effect.DirectionalLight1.Enabled = lights[1];
            effect.DirectionalLight1.DiffuseColor = new Vector3(0.5f, 0.5f, 0.5f);
            effect.DirectionalLight1.Direction = Vector3.Normalize(new Vector3(-1, -1, 0));
            effect.DirectionalLight1.SpecularColor = new Vector3(1f, 1f, 1f);

            effect.DirectionalLight2.Enabled = lights[2];
            effect.DirectionalLight2.DiffuseColor = new Vector3(0.3f, 0.3f, 0.3f);
            effect.DirectionalLight2.Direction = Vector3.Normalize(new Vector3(-1, -1, -1));
            effect.DirectionalLight2.SpecularColor = new Vector3(0.3f, 0.3f, 0.3f);

            effect.LightingEnabled = true;
        }
        #endregion
    }
}
