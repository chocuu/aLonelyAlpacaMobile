using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Anonym.Isometric;

/**
 * Model of a single block in the game.
 */
public class Block : IComparable {
    public enum BlockType {GRASS, LAVA, MOVEABLE, WIN, NONE, WALL};
    public BlockType b_type;
    public int p_x; public int p_y; public int p_z; // position
    public GridCoordinates coord_obj;

    /**
     * Creates a Block type. 
     */
    public Block(BlockType bt, Vector3 coords, GridCoordinates obj) {
        b_type = bt;

        p_x = (int)coords.x;
        p_y = (int)coords.y;
        p_z = (int)coords.z;
        coord_obj = obj;
    }

    /**
     * Required to be able to enumerate between blocks.
     */
    public int CompareTo(object obj)
    {
        Block f = (Block)obj;
        return this.p_z.CompareTo(f.p_z);
    }

    public Vector3 getCoords() {
        return new Vector3(p_x, p_y, p_z);
    }

    public void setCoords(Vector3 coords) {
        p_x = (int)coords.x;
        p_y = (int)coords.y;
        p_z = (int)coords.z;
    }

    public void Move(Vector3 dest) {
        if(b_type != BlockType.MOVEABLE)
            return;
        coord_obj.MoveToWorldPosition(dest);
    }

    public void SetAnim(bool set) {
        if(b_type != BlockType.MOVEABLE)
            return;
        Animator sr = coord_obj.GetComponentInChildren<Animator>();
        sr.ResetTrigger("try");
        if(set)
            sr.SetTrigger("try");
    }

    public void Highlight() {
        if(coord_obj.GetComponent<Unclickable>() != null)
            coord_obj.GetComponent<Unclickable>().setCanBeDroppedOnColor();
    }

    public void Unhighlight() {
        if(coord_obj.GetComponent<Unclickable>() != null)
            coord_obj.GetComponent<Unclickable>().setNormalColor();
    }

    public void LowOpacity() {
        if(coord_obj.GetComponent<Unclickable>() != null)
            coord_obj.GetComponent<Unclickable>().setDropping();
    }
}
